using SP.Aplicacao.DTOs.Common;
using SP.Aplicacao.DTOs.Sessoes;
using SP.Dominio.Enums;

namespace SP.Aplicacao.Services.Interfaces;

/// <summary>
/// Interface do Application Service de Sessões
/// </summary>
public interface ISessaoAppService
{
    /// <summary>
    /// Cria uma nova sessão
    /// </summary>
    Task<ResultadoDto<SessaoDto>> CriarAsync(CriarSessaoDto criarSessaoDto);

    /// <summary>
    /// Atualiza uma sessão existente
    /// </summary>
    Task<ResultadoDto<SessaoDto>> AtualizarAsync(int id, AtualizarSessaoDto atualizarSessaoDto);

    /// <summary>
    /// Remove uma sessão (soft delete)
    /// </summary>
    Task<ResultadoDto<bool>> RemoverAsync(int id);

    /// <summary>
    /// Obtém todas as sessões
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterTodosAsync();

    /// <summary>
    /// Obtém uma sessão por ID
    /// </summary>
    Task<ResultadoDto<SessaoDto>> ObterPorIdAsync(int id);

    /// <summary>
    /// Obtém sessões de um cliente
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterPorClienteAsync(int clienteId);

    /// <summary>
    /// Obtém sessões por período
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);

    /// <summary>
    /// Obtém sessões de um cliente por período
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterPorClientePeriodoAsync(int clienteId, DateTime dataInicio, DateTime dataFim);

    /// <summary>
    /// Obtém sessões por status
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterPorStatusAsync(StatusSessao status);

    /// <summary>
    /// Obtém sessões de hoje
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesHojeAsync();

    /// <summary>
    /// Obtém sessões de uma data específica
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterPorDataAsync(DateTime data);

    /// <summary>
    /// Obtém sessões não pagas
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesNaoPagasAsync();

    /// <summary>
    /// Obtém sessões não pagas de um cliente
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesNaoPagasPorClienteAsync(int clienteId);

    /// <summary>
    /// Obtém sessões para faturamento do mês
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesParaFaturamentoAsync(int ano, int mes);

    /// <summary>
    /// Obtém sessões de um cliente no mês
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesClienteMesAsync(int clienteId, int ano, int mes);

    /// <summary>
    /// Obtém próximas sessões
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterProximasSessoesAsync();

    /// <summary>
    /// Obtém estatísticas de sessões
    /// </summary>
    Task<ResultadoDto<EstatisticasSessoesDto>> ObterEstatisticasAsync();

    /// <summary>
    /// Marca sessão como realizada
    /// </summary>
    Task<ResultadoDto<bool>> MarcarComoRealizadaAsync(int id, DateTime? dataHoraRealizada = null, int? duracaoRealMinutos = null, string? anotacoesClinicas = null);

    /// <summary>
    /// Marca sessão como paga
    /// </summary>
    Task<ResultadoDto<bool>> MarcarComoPagaAsync(int id, string formaPagamento);

    /// <summary>
    /// Marca sessões como pagas em lote
    /// </summary>
    Task<ResultadoDto<bool>> MarcarSessoesComoPagasAsync(List<int> sessaoIds, string formaPagamento);

    /// <summary>
    /// Cancela sessão
    /// </summary>
    Task<ResultadoDto<bool>> CancelarSessaoAsync(int id, string motivo, StatusSessao novoStatus);

    /// <summary>
    /// Cancela sessões em lote
    /// </summary>
    Task<ResultadoDto<bool>> CancelarSessoesAsync(List<int> sessaoIds, string motivo, StatusSessao novoStatus);

    /// <summary>
    /// Reagenda sessão
    /// </summary>
    Task<ResultadoDto<SessaoDto>> ReagendarSessaoAsync(int id, DateTime novaDataHora, string? motivo = null);

    /// <summary>
    /// Gera sessões recorrentes
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> GerarSessoesRecorrentesAsync(GerarSessoesRecorrentesDto gerarSessoesDto);

    /// <summary>
    /// Verifica conflito de horário
    /// </summary>
    Task<ResultadoDto<bool>> VerificarConflitoHorarioAsync(DateTime dataHora, int duracaoMinutos, int? sessaoIdExcluir = null);

    /// <summary>
    /// Obtém sessões que precisam de confirmação
    /// </summary>
    Task<ResultadoDto<List<SessaoResumoDto>>> ObterSessoesParaConfirmacaoAsync();
}
