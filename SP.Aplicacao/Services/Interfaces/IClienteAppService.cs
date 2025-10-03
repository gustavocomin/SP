using SP.Aplicacao.DTOs.Clientes;
using SP.Aplicacao.DTOs.Common;

namespace SP.Aplicacao.Services.Interfaces
{
    public interface IClienteAppService
    {
        // CRUD Básico
        Task<ResultadoDto<List<ClienteResumoDto>>> ObterTodosAsync();
        Task<ResultadoDto<ClienteDto>> ObterPorIdAsync(int id);
        Task<ResultadoDto<ClienteDto>> CriarAsync(CriarClienteDto criarClienteDto);
        Task<ResultadoDto<ClienteDto>> AtualizarAsync(AtualizarClienteDto atualizarClienteDto);
        Task<ResultadoDto> RemoverAsync(int id);

        // Consultas Específicas
        Task<ResultadoDto<ClienteDto>> ObterPorCPFAsync(string cpf);
        Task<ResultadoDto<List<ClienteResumoDto>>> ObterPorNomeAsync(string email);

        // Consultas Financeiras
        Task<ResultadoDto<List<ClienteResumoDto>>> ObterInadimplentesAsync();
        Task<ResultadoDto<List<ClienteResumoDto>>> ObterEmDiaAsync();
        Task<ResultadoDto<List<ClienteResumoDto>>> ObterComVencimentoHojeAsync();
        Task<ResultadoDto<List<ClienteResumoDto>>> ObterComVencimentoProximosDiasAsync(int dias = 7);
        
        // Estatísticas
        Task<ResultadoDto<EstatisticasClientesDto>> ObterEstatisticasAsync();
        
        // Operações em Lote
        Task<ResultadoDto> AtivarClientesAsync(List<int> ids);
        Task<ResultadoDto> DesativarClientesAsync(List<int> ids);
        Task<ResultadoDto> AtualizarStatusFinanceiroAsync(int clienteId, SP.Dominio.Enums.StatusFinanceiro novoStatus);
    }
}
