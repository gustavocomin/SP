using AutoMapper;
using FluentValidation;
using SP.Aplicacao.DTOs.Common;
using SP.Aplicacao.DTOs.Sessoes;
using SP.Aplicacao.Services.Interfaces;
using SP.Dominio.Enums;
using SP.Dominio.Sessoes;
using SP.Infraestrutura.Common.Exceptions;
using SP.Infraestrutura.Entities.Clientes;
using SP.Infraestrutura.Entities.Sessoes;
using SP.Infraestrutura.UnitOfWork;

namespace SP.Aplicacao.Services;

/// <summary>
/// Application Service para gerenciamento de sessões
/// </summary>
public class SessaoAppService(
    ISessaoRepository sessaoRepository,
    IClienteRepository clienteRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IValidator<CriarSessaoDto> criarSessaoValidator,
    IValidator<AtualizarSessaoDto> atualizarSessaoValidator,
    IValidator<GerarSessoesRecorrentesDto> gerarSessoesValidator) : ISessaoAppService
{
    public async Task<ResultadoDto<SessaoDto>> CriarAsync(CriarSessaoDto criarSessaoDto)
    {
        try
        {
            // Validar dados de entrada
            var validationResult = await criarSessaoValidator.ValidateAsync(criarSessaoDto);
            if (!validationResult.IsValid)
            {
                return ResultadoDto<SessaoDto>.ComErros(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            // Verificar se o cliente existe
            var clienteExiste = await clienteRepository.ExisteAsync(criarSessaoDto.ClienteId);
            if (!clienteExiste)
            {
                return ResultadoDto<SessaoDto>.ComErro("Cliente não encontrado");
            }

            // Verificar conflito de horário
            var temConflito = await sessaoRepository.VerificarConflitoHorarioAsync(
                criarSessaoDto.DataHoraAgendada, 
                criarSessaoDto.DuracaoMinutos);

            if (temConflito)
            {
                return ResultadoDto<SessaoDto>.ComErro("Já existe uma sessão agendada neste horário");
            }

            // Mapear DTO para entidade
            var sessao = mapper.Map<Sessao>(criarSessaoDto);

            // Adicionar sessão
            await sessaoRepository.AdicionarAsync(sessao);
            await unitOfWork.SaveChangesAsync();

            // Buscar sessão criada com dados completos
            var sessaoCriada = await sessaoRepository.ObterPorIdAsync(sessao.Id);
            var sessaoDto = mapper.Map<SessaoDto>(sessaoCriada);

            return ResultadoDto<SessaoDto>.ComSucesso(sessaoDto, "Sessão criada com sucesso");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return ResultadoDto<SessaoDto>.ComErro($"Erro ao criar sessão: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<SessaoDto>> AtualizarAsync(int id, AtualizarSessaoDto atualizarSessaoDto)
    {
        try
        {
            // Validar dados de entrada
            var validationResult = await atualizarSessaoValidator.ValidateAsync(atualizarSessaoDto);
            if (!validationResult.IsValid)
            {
                return ResultadoDto<SessaoDto>.ComErros(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            // Buscar sessão existente
            var sessaoExistente = await sessaoRepository.ObterPorIdAsync(id);
            if (sessaoExistente == null)
            {
                return ResultadoDto<SessaoDto>.ComErro("Sessão não encontrada");
            }

            // Verificar conflito de horário (excluindo a própria sessão)
            var temConflito = await sessaoRepository.VerificarConflitoHorarioAsync(
                atualizarSessaoDto.DataHoraAgendada, 
                atualizarSessaoDto.DuracaoMinutos, 
                id);

            if (temConflito)
            {
                return ResultadoDto<SessaoDto>.ComErro("Já existe uma sessão agendada neste horário");
            }

            // Mapear alterações
            mapper.Map(atualizarSessaoDto, sessaoExistente);

            // Atualizar sessão
            sessaoRepository.Atualizar(sessaoExistente);
            await unitOfWork.SaveChangesAsync();

            // Retornar sessão atualizada
            var sessaoAtualizada = await sessaoRepository.ObterPorIdAsync(id);
            var sessaoDto = mapper.Map<SessaoDto>(sessaoAtualizada);

            return ResultadoDto<SessaoDto>.ComSucesso(sessaoDto, "Sessão atualizada com sucesso");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return ResultadoDto<SessaoDto>.ComErro($"Erro ao atualizar sessão: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<bool>> RemoverAsync(int id)
    {
        try
        {
            var sessao = await sessaoRepository.ObterPorIdAsync(id);
            if (sessao == null)
            {
                return ResultadoDto<bool>.ComErro("Sessão não encontrada");
            }

            // Soft delete
            sessao.Ativo = false;
            sessao.DataUltimaAtualizacao = DateTime.Now;

            sessaoRepository.Atualizar(sessao);
            await unitOfWork.SaveChangesAsync();

            return ResultadoDto<bool>.ComSucesso(true, "Sessão removida com sucesso");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return ResultadoDto<bool>.ComErro($"Erro ao remover sessão: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterTodosAsync()
    {
        try
        {
            var sessoes = await sessaoRepository.ObterTodosAsync();
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<SessaoDto>> ObterPorIdAsync(int id)
    {
        try
        {
            var sessao = await sessaoRepository.ObterPorIdAsync(id);
            if (sessao == null)
            {
                return ResultadoDto<SessaoDto>.ComErro("Sessão não encontrada");
            }

            var sessaoDto = mapper.Map<SessaoDto>(sessao);
            return ResultadoDto<SessaoDto>.ComSucesso(sessaoDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<SessaoDto>.ComErro($"Erro ao obter sessão: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterPorClienteAsync(int clienteId)
    {
        try
        {
            var sessoes = await sessaoRepository.ObterSessoesPorClienteAsync(clienteId);
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões do cliente: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        try
        {
            var sessoes = await sessaoRepository.ObterSessoesPorPeriodoAsync(dataInicio, dataFim);
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões por período: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterPorClientePeriodoAsync(int clienteId, DateTime dataInicio, DateTime dataFim)
    {
        try
        {
            var sessoes = await sessaoRepository.ObterSessoesPorClientePeriodoAsync(clienteId, dataInicio, dataFim);
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões do cliente por período: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterPorStatusAsync(StatusSessao status)
    {
        try
        {
            var sessoes = await sessaoRepository.ObterSessoesPorStatusAsync(status);
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões por status: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesHojeAsync()
    {
        try
        {
            var sessoes = await sessaoRepository.ObterSessoesHojeAsync();
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões de hoje: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterPorDataAsync(DateTime data)
    {
        try
        {
            var sessoes = await sessaoRepository.ObterSessoesPorDataAsync(data);
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões por data: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesNaoPagasAsync()
    {
        try
        {
            var sessoes = await sessaoRepository.ObterSessoesNaoPagasAsync();
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões não pagas: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesNaoPagasPorClienteAsync(int clienteId)
    {
        try
        {
            var sessoes = await sessaoRepository.ObterSessoesNaoPagasPorClienteAsync(clienteId);
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões não pagas do cliente: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesParaFaturamentoAsync(int ano, int mes)
    {
        try
        {
            var sessoes = await sessaoRepository.ObterSessoesParaFaturamentoAsync(ano, mes);
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões para faturamento: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesClienteMesAsync(int clienteId, int ano, int mes)
    {
        try
        {
            var sessoes = await sessaoRepository.ObterSessoesClienteMesAsync(clienteId, ano, mes);
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões do cliente no mês: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterProximasSessoesAsync()
    {
        try
        {
            var sessoes = await sessaoRepository.ObterProximasSessoesAsync();
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter próximas sessões: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<EstatisticasSessoesDto>> ObterEstatisticasAsync()
    {
        try
        {
            var agora = DateTime.Now;
            var inicioMes = new DateTime(agora.Year, agora.Month, 1);
            var fimMes = inicioMes.AddMonths(1).AddDays(-1);

            // Obter dados para estatísticas
            var todasSessoes = await sessaoRepository.ObterTodosAsync();
            var sessoesPorStatus = await sessaoRepository.ContarSessoesPorStatusAsync();
            var sessoesHoje = await sessaoRepository.ObterSessoesHojeAsync();
            var proximasSessoes = await sessaoRepository.ObterProximasSessoesAsync();
            var sessoesNaoPagas = await sessaoRepository.ObterSessoesNaoPagasAsync();
            var valorTotalRealizado = await sessaoRepository.ObterValorTotalPeriodoAsync(inicioMes, fimMes);
            var valorTotalNaoPago = await sessaoRepository.ObterValorTotalNaoPagasAsync();
            var estatisticasPorCliente = await sessaoRepository.ObterEstatisticasSessoesPorClienteAsync(inicioMes, fimMes);

            var estatisticas = new EstatisticasSessoesDto
            {
                TotalSessoes = todasSessoes.Count,
                SessoesAgendadas = sessoesPorStatus.GetValueOrDefault(StatusSessao.Agendada, 0),
                SessoesRealizadas = sessoesPorStatus.GetValueOrDefault(StatusSessao.Realizada, 0),
                SessoesCanceladas = sessoesPorStatus.GetValueOrDefault(StatusSessao.CanceladaCliente, 0) +
                                  sessoesPorStatus.GetValueOrDefault(StatusSessao.CanceladaPsicologo, 0),
                SessoesHoje = sessoesHoje.Count,
                SessoesProximosDias = proximasSessoes.Count,
                SessoesNaoPagas = sessoesNaoPagas.Count,
                ValorTotalRealizado = valorTotalRealizado,
                ValorTotalNaoPago = valorTotalNaoPago,
                ValorMedioSessao = todasSessoes.Any() ? todasSessoes.Average(s => s.Valor) : 0,
                SessoesPorStatus = sessoesPorStatus,
                SessoesPorCliente = estatisticasPorCliente
            };

            return ResultadoDto<EstatisticasSessoesDto>.ComSucesso(estatisticas);
        }
        catch (Exception ex)
        {
            return ResultadoDto<EstatisticasSessoesDto>.ComErro($"Erro ao obter estatísticas: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<bool>> MarcarComoRealizadaAsync(int id, DateTime? dataHoraRealizada = null, int? duracaoRealMinutos = null, string? anotacoesClinicas = null)
    {
        try
        {
            var sessao = await sessaoRepository.ObterPorIdAsync(id);
            if (sessao == null)
            {
                return ResultadoDto<bool>.ComErro("Sessão não encontrada");
            }

            sessao.Status = StatusSessao.Realizada;
            sessao.DataHoraRealizada = dataHoraRealizada ?? DateTime.Now;
            sessao.DuracaoRealMinutos = duracaoRealMinutos;
            sessao.AnotacoesClinicas = anotacoesClinicas;
            sessao.DataUltimaAtualizacao = DateTime.Now;

            sessaoRepository.Atualizar(sessao);
            await unitOfWork.SaveChangesAsync();

            return ResultadoDto<bool>.ComSucesso(true, "Sessão marcada como realizada");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return ResultadoDto<bool>.ComErro($"Erro ao marcar sessão como realizada: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<bool>> MarcarComoPagaAsync(int id, string formaPagamento)
    {
        try
        {
            var sessao = await sessaoRepository.ObterPorIdAsync(id);
            if (sessao == null)
            {
                return ResultadoDto<bool>.ComErro("Sessão não encontrada");
            }

            if (sessao.Status != StatusSessao.Realizada)
            {
                return ResultadoDto<bool>.ComErro("Apenas sessões realizadas podem ser marcadas como pagas");
            }

            sessao.Pago = true;
            sessao.DataPagamento = DateTime.Now;
            sessao.FormaPagamento = formaPagamento;
            sessao.DataUltimaAtualizacao = DateTime.Now;

            sessaoRepository.Atualizar(sessao);
            await unitOfWork.SaveChangesAsync();

            return ResultadoDto<bool>.ComSucesso(true, "Sessão marcada como paga");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return ResultadoDto<bool>.ComErro($"Erro ao marcar sessão como paga: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<bool>> MarcarSessoesComoPagasAsync(List<int> sessaoIds, string formaPagamento)
    {
        try
        {
            if (!sessaoIds.Any())
            {
                return ResultadoDto<bool>.ComErro("Nenhuma sessão selecionada");
            }

            await sessaoRepository.MarcarSessoesComoPagasAsync(sessaoIds, formaPagamento);

            return ResultadoDto<bool>.ComSucesso(true, $"{sessaoIds.Count} sessões marcadas como pagas");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return ResultadoDto<bool>.ComErro($"Erro ao marcar sessões como pagas: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<bool>> CancelarSessaoAsync(int id, string motivo, StatusSessao novoStatus)
    {
        try
        {
            if (novoStatus != StatusSessao.CanceladaCliente && novoStatus != StatusSessao.CanceladaPsicologo)
            {
                return ResultadoDto<bool>.ComErro("Status de cancelamento inválido");
            }

            var sessao = await sessaoRepository.ObterPorIdAsync(id);
            if (sessao == null)
            {
                return ResultadoDto<bool>.ComErro("Sessão não encontrada");
            }

            sessao.Status = novoStatus;
            sessao.MotivoCancelamento = motivo;
            sessao.DataUltimaAtualizacao = DateTime.Now;

            sessaoRepository.Atualizar(sessao);
            await unitOfWork.SaveChangesAsync();

            return ResultadoDto<bool>.ComSucesso(true, "Sessão cancelada com sucesso");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return ResultadoDto<bool>.ComErro($"Erro ao cancelar sessão: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<bool>> CancelarSessoesAsync(List<int> sessaoIds, string motivo, StatusSessao novoStatus)
    {
        try
        {
            if (novoStatus != StatusSessao.CanceladaCliente && novoStatus != StatusSessao.CanceladaPsicologo)
            {
                return ResultadoDto<bool>.ComErro("Status de cancelamento inválido");
            }

            if (!sessaoIds.Any())
            {
                return ResultadoDto<bool>.ComErro("Nenhuma sessão selecionada");
            }

            await sessaoRepository.CancelarSessoesAsync(sessaoIds, motivo, novoStatus);

            return ResultadoDto<bool>.ComSucesso(true, $"{sessaoIds.Count} sessões canceladas");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return ResultadoDto<bool>.ComErro($"Erro ao cancelar sessões: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<SessaoDto>> ReagendarSessaoAsync(int id, DateTime novaDataHora, string? motivo = null)
    {
        try
        {
            var sessaoOriginal = await sessaoRepository.ObterPorIdAsync(id);
            if (sessaoOriginal == null)
            {
                return ResultadoDto<SessaoDto>.ComErro("Sessão não encontrada");
            }

            // Verificar conflito de horário
            var temConflito = await sessaoRepository.VerificarConflitoHorarioAsync(
                novaDataHora,
                sessaoOriginal.DuracaoMinutos);

            if (temConflito)
            {
                return ResultadoDto<SessaoDto>.ComErro("Já existe uma sessão agendada neste horário");
            }

            // Marcar sessão original como reagendada
            sessaoOriginal.Status = StatusSessao.Reagendada;
            sessaoOriginal.MotivoCancelamento = motivo ?? "Reagendamento";
            sessaoOriginal.DataUltimaAtualizacao = DateTime.Now;

            // Criar nova sessão
            var novaSessao = new Sessao
            {
                ClienteId = sessaoOriginal.ClienteId,
                DataHoraAgendada = novaDataHora,
                DuracaoMinutos = sessaoOriginal.DuracaoMinutos,
                Valor = sessaoOriginal.Valor,
                Status = StatusSessao.Agendada,
                Periodicidade = sessaoOriginal.Periodicidade,
                Observacoes = sessaoOriginal.Observacoes,
                ConsiderarFaturamento = sessaoOriginal.ConsiderarFaturamento,
                SessaoOriginalId = sessaoOriginal.Id,
                DataCriacao = DateTime.Now,
                Ativo = true
            };

            sessaoRepository.Atualizar(sessaoOriginal);
            await sessaoRepository.AdicionarAsync(novaSessao);
            await unitOfWork.SaveChangesAsync();

            // Buscar nova sessão com dados completos
            var novaSessaoCompleta = await sessaoRepository.ObterPorIdAsync(novaSessao.Id);
            var sessaoDto = mapper.Map<SessaoDto>(novaSessaoCompleta);

            return ResultadoDto<SessaoDto>.ComSucesso(sessaoDto, "Sessão reagendada com sucesso");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return ResultadoDto<SessaoDto>.ComErro($"Erro ao reagendar sessão: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> GerarSessoesRecorrentesAsync(GerarSessoesRecorrentesDto gerarSessoesDto)
    {
        try
        {
            // Validar dados de entrada
            var validationResult = await gerarSessoesValidator.ValidateAsync(gerarSessoesDto);
            if (!validationResult.IsValid)
            {
                return ResultadoDto<List<SessaoResumoDto>>.ComErros(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            // Verificar se o cliente existe
            var clienteExiste = await clienteRepository.ExisteAsync(gerarSessoesDto.ClienteId);
            if (!clienteExiste)
            {
                return ResultadoDto<List<SessaoResumoDto>>.ComErro("Cliente não encontrado");
            }

            // Gerar sessões recorrentes
            var sessoesGeradas = await sessaoRepository.GerarSessoesRecorrentesAsync(
                gerarSessoesDto.ClienteId,
                gerarSessoesDto.DataInicio,
                gerarSessoesDto.DataFim,
                gerarSessoesDto.Periodicidade,
                gerarSessoesDto.HorarioSessao,
                gerarSessoesDto.Valor);

            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoesGeradas);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto, $"{sessoesGeradas.Count} sessões geradas com sucesso");
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao gerar sessões recorrentes: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<bool>> VerificarConflitoHorarioAsync(DateTime dataHora, int duracaoMinutos, int? sessaoIdExcluir = null)
    {
        try
        {
            var temConflito = await sessaoRepository.VerificarConflitoHorarioAsync(dataHora, duracaoMinutos, sessaoIdExcluir);
            return ResultadoDto<bool>.ComSucesso(temConflito);
        }
        catch (Exception ex)
        {
            return ResultadoDto<bool>.ComErro($"Erro ao verificar conflito de horário: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesParaConfirmacaoAsync()
    {
        try
        {
            var sessoes = await sessaoRepository.ObterSessoesParaConfirmacaoAsync();
            var sessoesDto = mapper.Map<List<SessaoResumoDto>>(sessoes);

            return ResultadoDto<List<SessaoResumoDto>>.ComSucesso(sessoesDto);
        }
        catch (Exception ex)
        {
            return ResultadoDto<List<SessaoResumoDto>>.ComErro($"Erro ao obter sessões para confirmação: {ex.Message}");
        }
    }
}
