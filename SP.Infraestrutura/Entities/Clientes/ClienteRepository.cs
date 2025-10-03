using Microsoft.EntityFrameworkCore;
using SP.Dominio.Clientes;
using SP.Dominio.Enums;
using SP.Infraestrutura.Data.Context;
using SP.Infraestrutura.Common.Base;

namespace SP.Infraestrutura.Entities.Clientes
{
    public class ClienteRepository(SPContext context) : RepositoryBase<Cliente>(context), IClienteRepository
    {
        public async Task<Cliente?> ObterPorCPFAsync(string cpf) =>
            await DbSet.FirstOrDefaultAsync(c => c.CPF == cpf);

        public async Task<Cliente?> ObterPorEmailAsync(string email) =>
            await DbSet.FirstOrDefaultAsync(c => c.Email == email);

        public async Task<List<Cliente>> ObterPorStatusFinanceiroAsync(StatusFinanceiro status) =>
            await DbSet
                .Where(c => c.StatusFinanceiro == status && c.Ativo)
                .OrderBy(c => c.Nome)
                .ToListAsync();

        public async Task<List<Cliente>> BuscarPorNomeAsync(string nome) =>
            await DbSet
                .Where(c => c.Nome.Contains(nome) && c.Ativo)
                .OrderBy(c => c.Nome)
                .ToListAsync();

        public async Task<List<Cliente>> ObterClientesInadimplentesAsync() =>
            await DbSet
                .Where(c => c.StatusFinanceiro == StatusFinanceiro.Inadimplente && c.Ativo)
                .OrderBy(c => c.Nome)
                .ToListAsync();

        public async Task<List<Cliente>> ObterClientesComVencimentoHojeAsync()
        {
            var hoje = DateTime.Today.Day;
            return await DbSet
                .Where(c => c.DiaVencimento.HasValue && c.DiaVencimento.Value == hoje && c.Ativo)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<List<Cliente>> ObterClientesComVencimentoAsync(int dia) =>
            await DbSet
                .Where(c => c.DiaVencimento.HasValue && c.DiaVencimento.Value == dia && c.Ativo)
                .OrderBy(c => c.Nome)
                .ToListAsync();

        public async Task<bool> ExisteCPFAsync(string cpf) =>
            await DbSet.AnyAsync(c => c.CPF == cpf);

        public async Task<bool> ExisteEmailAsync(string email) =>
            await DbSet.AnyAsync(c => c.Email == email);

        public async Task<bool> ExisteCPFAsync(string cpf, int idExcluir) =>
            await DbSet.AnyAsync(c => c.CPF == cpf && c.Id != idExcluir);

        public async Task<bool> ExisteEmailAsync(string email, int idExcluir) =>
            await DbSet.AnyAsync(c => c.Email == email && c.Id != idExcluir);

        public async Task<int> ContarPorStatusAsync(StatusFinanceiro status) =>
            await DbSet.CountAsync(c => c.StatusFinanceiro == status && c.Ativo);

        public async Task<decimal> ObterValorTotalSessoesAsync() =>
            await DbSet
                .Where(c => c.Ativo)
                .SumAsync(c => c.ValorSessao);

        // Override para incluir atualização da data de modificação
        public override void Atualizar(Cliente cliente)
        {
            cliente.DataUltimaAtualizacao = DateTime.Now;
            base.Atualizar(cliente);
        }

        public override void AtualizarRange(IEnumerable<Cliente> clientes)
        {
            foreach (var cliente in clientes)
            {
                cliente.DataUltimaAtualizacao = DateTime.Now;
            }
            base.AtualizarRange(clientes);
        }

        // Métodos adicionais específicos
        public async Task<List<Cliente>> ObterClientesPorStatusAsync(StatusFinanceiro status) =>
            await DbSet.Where(c => c.StatusFinanceiro == status && c.Ativo).ToListAsync();

        public async Task<List<Cliente>> ObterClientesComVencimentoProximosDiasAsync(int dias)
        {
            var hoje = DateTime.Today.Day;
            var diasFuturos = Enumerable.Range(1, dias)
                .Select(d => DateTime.Today.AddDays(d).Day)
                .ToList();

            return await DbSet
                .Where(c => c.Ativo && c.DiaVencimento.HasValue && diasFuturos.Contains(c.DiaVencimento.Value))
                .ToListAsync();
        }

        public async Task<int> ContarInativosAsync() =>
            await DbSet.CountAsync(c => !c.Ativo);

        public async Task<int> ContarClientesComVencimentoHojeAsync()
        {
            var hoje = DateTime.Today.Day;
            return await DbSet.CountAsync(c => c.Ativo && c.DiaVencimento.HasValue && c.DiaVencimento.Value == hoje);
        }

        public async Task<int> ContarClientesComVencimentoProximosDiasAsync(int dias)
        {
            var hoje = DateTime.Today.Day;
            var diasFuturos = Enumerable.Range(1, dias)
                .Select(d => DateTime.Today.AddDays(d).Day)
                .ToList();

            return await DbSet
                .CountAsync(c => c.Ativo && c.DiaVencimento.HasValue && diasFuturos.Contains(c.DiaVencimento.Value));
        }

        public async Task AtivarClientesAsync(List<int> ids)
        {
            var clientes = await DbSet.Where(c => ids.Contains(c.Id)).ToListAsync();
            foreach (var cliente in clientes)
            {
                cliente.Ativo = true;
                cliente.DataUltimaAtualizacao = DateTime.Now;
            }
        }

        public async Task DesativarClientesAsync(List<int> ids)
        {
            var clientes = await DbSet.Where(c => ids.Contains(c.Id)).ToListAsync();
            foreach (var cliente in clientes)
            {
                cliente.Ativo = false;
                cliente.DataUltimaAtualizacao = DateTime.Now;
            }
        }
    }
}
