using Microsoft.EntityFrameworkCore;
using SP.Dominio.Enums;
using SP.Dominio.Sessoes;
using SP.Dominio.Clientes;
using SP.Infraestrutura.Common.Base;
using SP.Infraestrutura.Data.Context;

namespace SP.Infraestrutura.Entities.Sessoes;

/// <summary>
/// Implementação do repositório de sessões
/// </summary>
public class SessaoRepository(SPContext context) : RepositoryBase<Sessao>(context), ISessaoRepository
{
    public async Task<List<Sessao>> ObterSessoesPorClienteAsync(int clienteId)
    {
        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.ClienteId == clienteId && s.Ativo)
            .OrderByDescending(s => s.DataHoraAgendada)
            .ToListAsync();
    }

    public async Task<List<Sessao>> ObterSessoesPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.DataHoraAgendada >= dataInicio && 
                       s.DataHoraAgendada <= dataFim && 
                       s.Ativo)
            .OrderBy(s => s.DataHoraAgendada)
            .ToListAsync();
    }

    public async Task<List<Sessao>> ObterSessoesPorClientePeriodoAsync(int clienteId, DateTime dataInicio, DateTime dataFim)
    {
        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.ClienteId == clienteId &&
                       s.DataHoraAgendada >= dataInicio && 
                       s.DataHoraAgendada <= dataFim && 
                       s.Ativo)
            .OrderBy(s => s.DataHoraAgendada)
            .ToListAsync();
    }

    public async Task<List<Sessao>> ObterSessoesPorStatusAsync(StatusSessao status)
    {
        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.Status == status && s.Ativo)
            .OrderBy(s => s.DataHoraAgendada)
            .ToListAsync();
    }

    public async Task<List<Sessao>> ObterSessoesHojeAsync()
    {
        var hoje = DateTime.Today;
        var amanha = hoje.AddDays(1);

        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.DataHoraAgendada >= hoje && 
                       s.DataHoraAgendada < amanha && 
                       s.Ativo)
            .OrderBy(s => s.DataHoraAgendada)
            .ToListAsync();
    }

    public async Task<List<Sessao>> ObterSessoesPorDataAsync(DateTime data)
    {
        var inicioData = data.Date;
        var fimData = inicioData.AddDays(1);

        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.DataHoraAgendada >= inicioData && 
                       s.DataHoraAgendada < fimData && 
                       s.Ativo)
            .OrderBy(s => s.DataHoraAgendada)
            .ToListAsync();
    }

    public async Task<List<Sessao>> ObterSessoesNaoPagasAsync()
    {
        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => !s.Pago && 
                       s.Status == StatusSessao.Realizada && 
                       s.ConsiderarFaturamento && 
                       s.Ativo)
            .OrderBy(s => s.DataHoraRealizada)
            .ToListAsync();
    }

    public async Task<List<Sessao>> ObterSessoesNaoPagasPorClienteAsync(int clienteId)
    {
        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.ClienteId == clienteId &&
                       !s.Pago && 
                       s.Status == StatusSessao.Realizada && 
                       s.ConsiderarFaturamento && 
                       s.Ativo)
            .OrderBy(s => s.DataHoraRealizada)
            .ToListAsync();
    }

    public async Task<List<Sessao>> ObterSessoesParaFaturamentoAsync(int ano, int mes)
    {
        var dataInicio = new DateTime(ano, mes, 1);
        var dataFim = dataInicio.AddMonths(1);

        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.DataHoraRealizada >= dataInicio && 
                       s.DataHoraRealizada < dataFim &&
                       s.Status == StatusSessao.Realizada && 
                       s.ConsiderarFaturamento && 
                       s.Ativo)
            .OrderBy(s => s.DataHoraRealizada)
            .ToListAsync();
    }

    public async Task<List<Sessao>> ObterSessoesClienteMesAsync(int clienteId, int ano, int mes)
    {
        var dataInicio = new DateTime(ano, mes, 1);
        var dataFim = dataInicio.AddMonths(1);

        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.ClienteId == clienteId &&
                       s.DataHoraRealizada >= dataInicio && 
                       s.DataHoraRealizada < dataFim &&
                       s.Status == StatusSessao.Realizada && 
                       s.ConsiderarFaturamento && 
                       s.Ativo)
            .OrderBy(s => s.DataHoraRealizada)
            .ToListAsync();
    }

    public async Task<bool> VerificarConflitoHorarioAsync(DateTime dataHora, int duracaoMinutos, int? sessaoIdExcluir = null)
    {
        var inicioSessao = dataHora;
        var fimSessao = dataHora.AddMinutes(duracaoMinutos);

        var query = DbSet.Where(s => s.Ativo && 
                                    s.Status != StatusSessao.CanceladaCliente && 
                                    s.Status != StatusSessao.CanceladaPsicologo);

        if (sessaoIdExcluir.HasValue)
        {
            query = query.Where(s => s.Id != sessaoIdExcluir.Value);
        }

        return await query.AnyAsync(s => 
            (s.DataHoraAgendada < fimSessao && 
             s.DataHoraAgendada.AddMinutes(s.DuracaoMinutos) > inicioSessao));
    }

    public async Task<List<Sessao>> ObterProximasSessoesAsync()
    {
        var agora = DateTime.Now;
        var proximosSete = agora.AddDays(7);

        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.DataHoraAgendada >= agora && 
                       s.DataHoraAgendada <= proximosSete && 
                       s.Status == StatusSessao.Agendada && 
                       s.Ativo)
            .OrderBy(s => s.DataHoraAgendada)
            .ToListAsync();
    }

    public async Task<Dictionary<StatusSessao, int>> ContarSessoesPorStatusAsync()
    {
        return await DbSet
            .Where(s => s.Ativo)
            .GroupBy(s => s.Status)
            .ToDictionaryAsync(g => g.Key, g => g.Count());
    }

    public async Task<decimal> ObterValorTotalPeriodoAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await DbSet
            .Where(s => s.DataHoraRealizada >= dataInicio && 
                       s.DataHoraRealizada <= dataFim &&
                       s.Status == StatusSessao.Realizada && 
                       s.ConsiderarFaturamento && 
                       s.Ativo)
            .SumAsync(s => s.Valor);
    }

    public async Task<decimal> ObterValorTotalNaoPagasAsync()
    {
        return await DbSet
            .Where(s => !s.Pago && 
                       s.Status == StatusSessao.Realizada && 
                       s.ConsiderarFaturamento && 
                       s.Ativo)
            .SumAsync(s => s.Valor);
    }

    public async Task<Dictionary<int, int>> ObterEstatisticasSessoesPorClienteAsync(DateTime dataInicio, DateTime dataFim)
    {
        return await DbSet
            .Where(s => s.DataHoraRealizada >= dataInicio && 
                       s.DataHoraRealizada <= dataFim &&
                       s.Status == StatusSessao.Realizada && 
                       s.Ativo)
            .GroupBy(s => s.ClienteId)
            .ToDictionaryAsync(g => g.Key, g => g.Count());
    }

    public async Task MarcarSessoesComoPagasAsync(List<int> sessaoIds, string formaPagamento)
    {
        var sessoes = await DbSet
            .Where(s => sessaoIds.Contains(s.Id) && s.Ativo)
            .ToListAsync();

        foreach (var sessao in sessoes)
        {
            sessao.Pago = true;
            sessao.DataPagamento = DateTime.Now;
            sessao.FormaPagamento = formaPagamento;
            sessao.DataUltimaAtualizacao = DateTime.Now;
        }

        await Context.SaveChangesAsync();
    }

    public async Task CancelarSessoesAsync(List<int> sessaoIds, string motivo, StatusSessao novoStatus)
    {
        var sessoes = await DbSet
            .Where(s => sessaoIds.Contains(s.Id) && s.Ativo)
            .ToListAsync();

        foreach (var sessao in sessoes)
        {
            sessao.Status = novoStatus;
            sessao.MotivoCancelamento = motivo;
            sessao.DataUltimaAtualizacao = DateTime.Now;
        }

        await Context.SaveChangesAsync();
    }

    public async Task<List<Sessao>> ObterSessoesParaConfirmacaoAsync()
    {
        var agora = DateTime.Now;
        var proximasVinteQuatro = agora.AddHours(24);

        return await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.DataHoraAgendada >= agora && 
                       s.DataHoraAgendada <= proximasVinteQuatro && 
                       s.Status == StatusSessao.Agendada && 
                       s.Ativo)
            .OrderBy(s => s.DataHoraAgendada)
            .ToListAsync();
    }

    public async Task<List<Sessao>> GerarSessoesRecorrentesAsync(int clienteId, DateTime dataInicio, DateTime dataFim, PeriodicidadeSessao periodicidade, TimeSpan horario, decimal valor)
    {
        var sessoes = new List<Sessao>();
        var dataAtual = dataInicio.Date.Add(horario);

        // Verifica se o cliente existe
        var clienteExiste = await Context.Set<SP.Dominio.Clientes.Cliente>()
            .AnyAsync(c => c.Id == clienteId && c.Ativo);

        if (!clienteExiste)
            return sessoes;

        while (dataAtual <= dataFim)
        {
            // Verifica se não há conflito de horário
            var temConflito = await VerificarConflitoHorarioAsync(dataAtual, 50);

            if (!temConflito)
            {
                var sessao = new Sessao
                {
                    ClienteId = clienteId,
                    DataHoraAgendada = dataAtual,
                    DuracaoMinutos = 50,
                    Valor = valor,
                    Status = StatusSessao.Agendada,
                    Periodicidade = periodicidade,
                    ConsiderarFaturamento = true,
                    DataCriacao = DateTime.Now,
                    Ativo = true
                };

                sessoes.Add(sessao);
            }

            // Calcula próxima data baseada na periodicidade
            dataAtual = periodicidade switch
            {
                PeriodicidadeSessao.Diario => dataAtual.AddDays(1),
                PeriodicidadeSessao.Bisemanal => dataAtual.AddDays(3.5), // Aproximadamente 2x por semana
                PeriodicidadeSessao.Semanal => dataAtual.AddDays(7),
                PeriodicidadeSessao.Quinzenal => dataAtual.AddDays(14),
                PeriodicidadeSessao.Mensal => dataAtual.AddMonths(1),
                PeriodicidadeSessao.Livre => dataFim.AddDays(1), // Para periodicidade livre, não gera automaticamente
                _ => dataAtual.AddDays(7)
            };
        }

        if (sessoes.Any())
        {
            await DbSet.AddRangeAsync(sessoes);
            await Context.SaveChangesAsync();
        }

        return sessoes;
    }

    // Implementação dos métodos para relatórios financeiros

    public async Task<(int TotalRealizadas, int TotalCanceladas, int TotalFaltas, decimal ValorFaturado, decimal ValorPago)>
        ObterDadosFinanceirosMensalAsync(int ano, int mes)
    {
        var dataInicio = new DateTime(ano, mes, 1);
        var dataFim = dataInicio.AddMonths(1).AddDays(-1);

        var sessoes = await DbSet
            .Where(s => s.DataHoraRealizada >= dataInicio &&
                       s.DataHoraRealizada <= dataFim &&
                       s.ConsiderarFaturamento &&
                       s.Ativo)
            .ToListAsync();

        var totalRealizadas = sessoes.Count(s => s.Status == StatusSessao.Realizada);
        var totalCanceladas = sessoes.Count(s => s.Status == StatusSessao.CanceladaCliente || s.Status == StatusSessao.CanceladaPsicologo);
        var totalFaltas = sessoes.Count(s => s.Status == StatusSessao.Falta);
        var valorFaturado = sessoes.Where(s => s.Status == StatusSessao.Realizada).Sum(s => s.Valor);
        var valorPago = sessoes.Where(s => s.Status == StatusSessao.Realizada && s.Pago).Sum(s => s.Valor);

        return (totalRealizadas, totalCanceladas, totalFaltas, valorFaturado, valorPago);
    }

    public async Task<List<(int ClienteId, string NomeCliente, int QuantidadeSessoes, decimal ValorFaturado, decimal ValorPago, DateTime? UltimaDataPagamento)>>
        ObterSessoesPorClienteMensalAsync(int ano, int mes)
    {
        var dataInicio = new DateTime(ano, mes, 1);
        var dataFim = dataInicio.AddMonths(1).AddDays(-1);

        var grupos = await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.DataHoraRealizada >= dataInicio &&
                       s.DataHoraRealizada <= dataFim &&
                       s.Status == StatusSessao.Realizada &&
                       s.ConsiderarFaturamento &&
                       s.Ativo)
            .ToListAsync();

        return grupos
            .GroupBy(s => new { s.ClienteId, s.Cliente.Nome })
            .Select(g => (
                g.Key.ClienteId,
                g.Key.Nome,
                g.Count(),
                g.Sum(s => s.Valor),
                g.Where(s => s.Pago).Sum(s => s.Valor),
                g.Where(s => s.Pago && s.DataPagamento.HasValue).Max(s => s.DataPagamento)
            ))
            .ToList();
    }

    public async Task<List<(string FormaPagamento, int QuantidadeSessoes, decimal ValorTotal)>>
        ObterSessoesPorFormaPagamentoMensalAsync(int ano, int mes)
    {
        var dataInicio = new DateTime(ano, mes, 1);
        var dataFim = dataInicio.AddMonths(1).AddDays(-1);

        var sessoes = await DbSet
            .Where(s => s.DataPagamento >= dataInicio &&
                       s.DataPagamento <= dataFim &&
                       s.Pago &&
                       s.Status == StatusSessao.Realizada &&
                       s.ConsiderarFaturamento &&
                       s.Ativo &&
                       !string.IsNullOrEmpty(s.FormaPagamento))
            .ToListAsync();

        return sessoes
            .GroupBy(s => s.FormaPagamento!)
            .Select(g => (g.Key, g.Count(), g.Sum(s => s.Valor)))
            .ToList();
    }

    public async Task<List<(int Id, string NomeCliente, DateTime DataSessao, decimal Valor)>>
        ObterSessoesNaoPagasMensalAsync(int ano, int mes)
    {
        var dataInicio = new DateTime(ano, mes, 1);
        var dataFim = dataInicio.AddMonths(1).AddDays(-1);

        var sessoes = await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.DataHoraRealizada >= dataInicio &&
                       s.DataHoraRealizada <= dataFim &&
                       s.Status == StatusSessao.Realizada &&
                       !s.Pago &&
                       s.ConsiderarFaturamento &&
                       s.Ativo)
            .OrderBy(s => s.DataHoraRealizada)
            .ToListAsync();

        return sessoes.Select(s => (s.Id, s.Cliente.Nome, s.DataHoraRealizada!.Value, s.Valor)).ToList();
    }

    public async Task<List<(int Mes, int QuantidadeSessoes, decimal ValorFaturado, decimal ValorPago)>>
        ObterDadosFinanceirosAnualAsync(int ano)
    {
        var dataInicio = new DateTime(ano, 1, 1);
        var dataFim = new DateTime(ano, 12, 31);

        var sessoes = await DbSet
            .Where(s => s.DataHoraRealizada >= dataInicio &&
                       s.DataHoraRealizada <= dataFim &&
                       s.Status == StatusSessao.Realizada &&
                       s.ConsiderarFaturamento &&
                       s.Ativo)
            .ToListAsync();

        return sessoes
            .GroupBy(s => s.DataHoraRealizada!.Value.Month)
            .Select(g => (
                g.Key,
                g.Count(),
                g.Sum(s => s.Valor),
                g.Where(s => s.Pago).Sum(s => s.Valor)
            ))
            .OrderBy(x => x.Item1)
            .ToList();
    }

    public async Task<List<(int ClienteId, string NomeCliente, decimal ValorEmAberto, int QuantidadeSessoes, DateTime? SessaoMaisAntiga)>>
        ObterClientesComMaiorDebitoAsync(int limite = 10)
    {
        var sessoes = await DbSet
            .Include(s => s.Cliente)
            .Where(s => s.Status == StatusSessao.Realizada &&
                       !s.Pago &&
                       s.ConsiderarFaturamento &&
                       s.Ativo)
            .ToListAsync();

        return sessoes
            .GroupBy(s => new { s.ClienteId, s.Cliente.Nome })
            .Select(g => (
                g.Key.ClienteId,
                g.Key.Nome,
                g.Sum(s => s.Valor),
                g.Count(),
                g.Min(s => s.DataHoraRealizada)
            ))
            .OrderByDescending(x => x.Item3)
            .Take(limite)
            .ToList();
    }

    public async Task<List<(int ClienteId, string NomeCliente, DateTime DataVencimento, decimal ValorAVencer)>>
        ObterProximosVencimentosAsync(int diasAntecedencia = 7)
    {
        var hoje = DateTime.Today;
        var dataLimite = hoje.AddDays(diasAntecedencia);

        // Buscar clientes com sessões não pagas e dia de vencimento configurado
        var clientesComVencimento = await Context.Clientes
            .Where(c => c.DiaVencimento.HasValue && c.Ativo)
            .Select(c => new { c.Id, c.Nome, c.DiaVencimento })
            .ToListAsync();

        var proximosVencimentos = new List<(int, string, DateTime, decimal)>();

        foreach (var cliente in clientesComVencimento)
        {
            // Calcular próxima data de vencimento
            var diaVencimento = cliente.DiaVencimento!.Value;
            var proximoVencimento = new DateTime(hoje.Year, hoje.Month, Math.Min(diaVencimento, DateTime.DaysInMonth(hoje.Year, hoje.Month)));

            if (proximoVencimento < hoje)
            {
                proximoVencimento = proximoVencimento.AddMonths(1);
                proximoVencimento = new DateTime(proximoVencimento.Year, proximoVencimento.Month,
                    Math.Min(diaVencimento, DateTime.DaysInMonth(proximoVencimento.Year, proximoVencimento.Month)));
            }

            if (proximoVencimento <= dataLimite)
            {
                // Buscar valor em aberto do cliente
                var valorEmAberto = await DbSet
                    .Where(s => s.ClienteId == cliente.Id &&
                               s.Status == StatusSessao.Realizada &&
                               !s.Pago &&
                               s.ConsiderarFaturamento &&
                               s.Ativo)
                    .SumAsync(s => s.Valor);

                if (valorEmAberto > 0)
                {
                    proximosVencimentos.Add((cliente.Id, cliente.Nome, proximoVencimento, valorEmAberto));
                }
            }
        }

        return proximosVencimentos.OrderBy(x => x.Item3).ToList();
    }
}
