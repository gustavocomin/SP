using SP.Dominio.Clientes;
using SP.Dominio.Enums;

namespace SP.Infraestrutura.Entities.Clientes
{
    public interface IClienteRepository
    {
        // Operações básicas
        Task<Cliente?> ObterPorIdAsync(int id);
        Task<Cliente> ObterPorIdObrigatorioAsync(int id);
        Task<List<Cliente>> ObterTodosAsync();
        Task<List<Cliente>> ObterTodosAtivosAsync();
        Task<List<Cliente>> ObterPorIdsAsync(IEnumerable<int> ids);
        Task AdicionarAsync(Cliente cliente);
        Task AdicionarRangeAsync(IEnumerable<Cliente> clientes);
        void Atualizar(Cliente cliente);
        void AtualizarRange(IEnumerable<Cliente> clientes);
        void Remover(Cliente cliente);
        void RemoverRange(IEnumerable<Cliente> clientes);
        Task<bool> RemoverPorIdAsync(int id);
        Task RemoverPorIdsAsync(List<int> ids);
        void RemoverLogico(Cliente cliente);

        // Consultas específicas
        Task<Cliente?> ObterPorCPFAsync(string cpf);
        Task<Cliente?> ObterPorEmailAsync(string email);
        Task<List<Cliente>> ObterPorStatusFinanceiroAsync(StatusFinanceiro status);
        Task<List<Cliente>> ObterClientesPorStatusAsync(StatusFinanceiro status);
        Task<List<Cliente>> BuscarPorNomeAsync(string nome);
        Task<List<Cliente>> ObterClientesInadimplentesAsync();
        Task<List<Cliente>> ObterClientesComVencimentoHojeAsync();
        Task<List<Cliente>> ObterClientesComVencimentoAsync(int dia);
        Task<List<Cliente>> ObterClientesComVencimentoProximosDiasAsync(int dias);

        // Validações
        Task<bool> ExisteAsync(int id);
        Task<bool> ExisteAtivoAsync(int id);
        Task<bool> ExisteCPFAsync(string cpf);
        Task<bool> ExisteEmailAsync(string email);
        Task<bool> ExisteCPFAsync(string cpf, int idExcluir);
        Task<bool> ExisteEmailAsync(string email, int idExcluir);

        // Estatísticas
        Task<int> ContarAsync();
        Task<int> ContarAtivosAsync();
        Task<int> ContarInativosAsync();
        Task<int> ContarPorStatusAsync(StatusFinanceiro status);
        Task<int> ContarClientesComVencimentoHojeAsync();
        Task<int> ContarClientesComVencimentoProximosDiasAsync(int dias);
        Task<decimal> ObterValorTotalSessoesAsync();

        // Operações em lote
        Task AtivarClientesAsync(List<int> ids);
        Task DesativarClientesAsync(List<int> ids);
    }
}
