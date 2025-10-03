using AutoMapper;
using FluentValidation;
using SP.Aplicacao.DTOs.Clientes;
using SP.Aplicacao.DTOs.Common;
using SP.Aplicacao.Services.Interfaces;
using SP.Dominio.Clientes;
using SP.Dominio.Enums;
using SP.Infraestrutura.Entities.Clientes;
using SP.Infraestrutura.UnitOfWork;

namespace SP.Aplicacao.Services
{
    public class ClienteAppService(
        IClienteRepository clienteRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IValidator<CriarClienteDto> criarClienteValidator,
        IValidator<AtualizarClienteDto> atualizarClienteValidator) : IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CriarClienteDto> _criarClienteValidator = criarClienteValidator;
        private readonly IValidator<AtualizarClienteDto> _atualizarClienteValidator = atualizarClienteValidator;

        public async Task<ResultadoDto<List<ClienteResumoDto>>> ObterTodosAsync()
        {
            try
            {
                var clientes = await _clienteRepository.ObterTodosAtivosAsync();
                var clientesDto = _mapper.Map<List<ClienteResumoDto>>(clientes);
                
                return ResultadoDto<List<ClienteResumoDto>>.ComSucesso(clientesDto);
            }
            catch (Exception ex)
            {
                return ResultadoDto<List<ClienteResumoDto>>.ComErro($"Erro ao obter clientes: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<ClienteDto>> ObterPorIdAsync(int id)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorIdAsync(id);
                if (cliente == null)
                {
                    return ResultadoDto<ClienteDto>.ComErro("Cliente não encontrado");
                }

                var clienteDto = _mapper.Map<ClienteDto>(cliente);
                return ResultadoDto<ClienteDto>.ComSucesso(clienteDto);
            }
            catch (Exception ex)
            {
                return ResultadoDto<ClienteDto>.ComErro($"Erro ao obter cliente: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<ClienteDto>> CriarAsync(CriarClienteDto criarClienteDto)
        {
            try
            {
                // Validação
                var validationResult = await _criarClienteValidator.ValidateAsync(criarClienteDto);
                if (!validationResult.IsValid)
                {
                    var erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return ResultadoDto<ClienteDto>.ComErros(erros);
                }

                // Verificar se CPF já existe
                if (await _clienteRepository.ExisteCPFAsync(criarClienteDto.CPF))
                {
                    return ResultadoDto<ClienteDto>.ComErro("CPF já cadastrado no sistema");
                }

                // Verificar se Email já existe
                if (await _clienteRepository.ExisteEmailAsync(criarClienteDto.Email))
                {
                    return ResultadoDto<ClienteDto>.ComErro("Email já cadastrado no sistema");
                }

                // Mapear e criar
                var cliente = _mapper.Map<Cliente>(criarClienteDto);
                await _clienteRepository.AdicionarAsync(cliente);
                await _unitOfWork.SaveChangesAsync();

                var clienteDto = _mapper.Map<ClienteDto>(cliente);
                return ResultadoDto<ClienteDto>.ComSucesso(clienteDto, "Cliente criado com sucesso");
            }
            catch (Exception ex)
            {
                return ResultadoDto<ClienteDto>.ComErro($"Erro ao criar cliente: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<ClienteDto>> AtualizarAsync(AtualizarClienteDto atualizarClienteDto)
        {
            try
            {
                // Validação
                var validationResult = await _atualizarClienteValidator.ValidateAsync(atualizarClienteDto);
                if (!validationResult.IsValid)
                {
                    var erros = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return ResultadoDto<ClienteDto>.ComErros(erros);
                }

                // Verificar se cliente existe
                var clienteExistente = await _clienteRepository.ObterPorIdAsync(atualizarClienteDto.Id);
                if (clienteExistente == null)
                {
                    return ResultadoDto<ClienteDto>.ComErro("Cliente não encontrado");
                }

                // Verificar se CPF já existe para outro cliente
                if (await _clienteRepository.ExisteCPFAsync(atualizarClienteDto.CPF, atualizarClienteDto.Id))
                {
                    return ResultadoDto<ClienteDto>.ComErro("CPF já cadastrado para outro cliente");
                }

                // Verificar se Email já existe para outro cliente
                if (await _clienteRepository.ExisteEmailAsync(atualizarClienteDto.Email, atualizarClienteDto.Id))
                {
                    return ResultadoDto<ClienteDto>.ComErro("Email já cadastrado para outro cliente");
                }

                // Mapear e atualizar
                _mapper.Map(atualizarClienteDto, clienteExistente);
                _clienteRepository.Atualizar(clienteExistente);
                await _unitOfWork.SaveChangesAsync();

                var clienteDto = _mapper.Map<ClienteDto>(clienteExistente);
                return ResultadoDto<ClienteDto>.ComSucesso(clienteDto, "Cliente atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return ResultadoDto<ClienteDto>.ComErro($"Erro ao atualizar cliente: {ex.Message}");
            }
        }

        public async Task<ResultadoDto> RemoverAsync(int id)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorIdAsync(id);
                if (cliente == null)
                {
                    return ResultadoDto.ComErro("Cliente não encontrado");
                }

                _clienteRepository.RemoverLogico(cliente);
                await _unitOfWork.SaveChangesAsync();

                return ResultadoDto.ComSucesso("Cliente removido com sucesso");
            }
            catch (Exception ex)
            {
                return ResultadoDto.ComErro($"Erro ao remover cliente: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<ClienteDto>> ObterPorCPFAsync(string cpf)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorCPFAsync(cpf);
                if (cliente == null)
                {
                    return ResultadoDto<ClienteDto>.ComErro("Cliente não encontrado");
                }

                var clienteDto = _mapper.Map<ClienteDto>(cliente);
                return ResultadoDto<ClienteDto>.ComSucesso(clienteDto);
            }
            catch (Exception ex)
            {
                return ResultadoDto<ClienteDto>.ComErro($"Erro ao obter cliente por CPF: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<List<ClienteResumoDto>>> ObterPorNomeAsync(string nome)
        {
            try
            {
                var clientes = await _clienteRepository.BuscarPorNomeAsync(nome);
                var clientesDto = _mapper.Map<List<ClienteResumoDto>>(clientes);
                
                return ResultadoDto<List<ClienteResumoDto>>.ComSucesso(clientesDto);
            }
            catch (Exception ex)
            {
                return ResultadoDto<List<ClienteResumoDto>>.ComErro($"Erro ao buscar clientes por nome: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<List<ClienteResumoDto>>> ObterInadimplentesAsync()
        {
            try
            {
                var clientes = await _clienteRepository.ObterClientesInadimplentesAsync();
                var clientesDto = _mapper.Map<List<ClienteResumoDto>>(clientes);
                
                return ResultadoDto<List<ClienteResumoDto>>.ComSucesso(clientesDto);
            }
            catch (Exception ex)
            {
                return ResultadoDto<List<ClienteResumoDto>>.ComErro($"Erro ao obter clientes inadimplentes: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<List<ClienteResumoDto>>> ObterEmDiaAsync()
        {
            try
            {
                var clientes = await _clienteRepository.ObterClientesPorStatusAsync(StatusFinanceiro.EmDia);
                var clientesDto = _mapper.Map<List<ClienteResumoDto>>(clientes);
                
                return ResultadoDto<List<ClienteResumoDto>>.ComSucesso(clientesDto);
            }
            catch (Exception ex)
            {
                return ResultadoDto<List<ClienteResumoDto>>.ComErro($"Erro ao obter clientes em dia: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<List<ClienteResumoDto>>> ObterComVencimentoHojeAsync()
        {
            try
            {
                var clientes = await _clienteRepository.ObterClientesComVencimentoHojeAsync();
                var clientesDto = _mapper.Map<List<ClienteResumoDto>>(clientes);
                
                return ResultadoDto<List<ClienteResumoDto>>.ComSucesso(clientesDto);
            }
            catch (Exception ex)
            {
                return ResultadoDto<List<ClienteResumoDto>>.ComErro($"Erro ao obter clientes com vencimento hoje: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<List<ClienteResumoDto>>> ObterComVencimentoProximosDiasAsync(int dias = 7)
        {
            try
            {
                var clientes = await _clienteRepository.ObterClientesComVencimentoProximosDiasAsync(dias);
                var clientesDto = _mapper.Map<List<ClienteResumoDto>>(clientes);
                
                return ResultadoDto<List<ClienteResumoDto>>.ComSucesso(clientesDto);
            }
            catch (Exception ex)
            {
                return ResultadoDto<List<ClienteResumoDto>>.ComErro($"Erro ao obter clientes com vencimento nos próximos {dias} dias: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<EstatisticasClientesDto>> ObterEstatisticasAsync()
        {
            try
            {
                var totalAtivos = await _clienteRepository.ContarAtivosAsync();
                var totalInativos = await _clienteRepository.ContarInativosAsync();
                var totalInadimplentes = await _clienteRepository.ContarPorStatusAsync(StatusFinanceiro.Inadimplente);
                var totalEmDia = await _clienteRepository.ContarPorStatusAsync(StatusFinanceiro.EmDia);
                var valorTotalSessoes = await _clienteRepository.ObterValorTotalSessoesAsync();
                var valorMedioSessao = totalAtivos > 0 ? valorTotalSessoes / totalAtivos : 0;
                var clientesVencimentoHoje = await _clienteRepository.ContarClientesComVencimentoHojeAsync();
                var clientesVencimentoProximos = await _clienteRepository.ContarClientesComVencimentoProximosDiasAsync(7);

                var estatisticas = new EstatisticasClientesDto
                {
                    TotalClientesAtivos = totalAtivos,
                    TotalClientesInativos = totalInativos,
                    TotalClientesInadimplentes = totalInadimplentes,
                    TotalClientesEmDia = totalEmDia,
                    ValorTotalSessoes = valorTotalSessoes,
                    ValorMedioSessao = valorMedioSessao,
                    ClientesComVencimentoHoje = clientesVencimentoHoje,
                    ClientesComVencimentoProximosDias = clientesVencimentoProximos
                };

                return ResultadoDto<EstatisticasClientesDto>.ComSucesso(estatisticas);
            }
            catch (Exception ex)
            {
                return ResultadoDto<EstatisticasClientesDto>.ComErro($"Erro ao obter estatísticas: {ex.Message}");
            }
        }

        public async Task<ResultadoDto> AtivarClientesAsync(List<int> ids)
        {
            try
            {
                await _clienteRepository.AtivarClientesAsync(ids);
                await _unitOfWork.SaveChangesAsync();

                return ResultadoDto.ComSucesso($"{ids.Count} cliente(s) ativado(s) com sucesso");
            }
            catch (Exception ex)
            {
                return ResultadoDto.ComErro($"Erro ao ativar clientes: {ex.Message}");
            }
        }

        public async Task<ResultadoDto> DesativarClientesAsync(List<int> ids)
        {
            try
            {
                await _clienteRepository.DesativarClientesAsync(ids);
                await _unitOfWork.SaveChangesAsync();

                return ResultadoDto.ComSucesso($"{ids.Count} cliente(s) desativado(s) com sucesso");
            }
            catch (Exception ex)
            {
                return ResultadoDto.ComErro($"Erro ao desativar clientes: {ex.Message}");
            }
        }

        public async Task<ResultadoDto> AtualizarStatusFinanceiroAsync(int clienteId, StatusFinanceiro novoStatus)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorIdAsync(clienteId);
                if (cliente == null)
                {
                    return ResultadoDto.ComErro("Cliente não encontrado");
                }

                cliente.StatusFinanceiro = novoStatus;
                _clienteRepository.Atualizar(cliente);
                await _unitOfWork.SaveChangesAsync();

                return ResultadoDto.ComSucesso("Status financeiro atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return ResultadoDto.ComErro($"Erro ao atualizar status financeiro: {ex.Message}");
            }
        }
    }
}
