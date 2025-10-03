using SP.Aplicacao.DTOs.Common;
using SP.Aplicacao.DTOs.Financeiro;

namespace SP.Aplicacao.Services.Interfaces;

/// <summary>
/// Interface para serviços financeiros
/// </summary>
public interface IFinanceiroAppService
{
    /// <summary>
    /// Obtém relatório financeiro mensal
    /// </summary>
    Task<ResultadoDto<RelatorioFinanceiroMensalDto>> ObterRelatorioMensalAsync(int ano, int mes);

    /// <summary>
    /// Obtém relatório financeiro anual
    /// </summary>
    Task<ResultadoDto<RelatorioFinanceiroAnualDto>> ObterRelatorioAnualAsync(int ano);

    /// <summary>
    /// Obtém dashboard financeiro
    /// </summary>
    Task<ResultadoDto<DashboardFinanceiroDto>> ObterDashboardFinanceiroAsync();

    /// <summary>
    /// Obtém relatório do mês atual
    /// </summary>
    Task<ResultadoDto<RelatorioFinanceiroMensalDto>> ObterRelatorioMesAtualAsync();

    /// <summary>
    /// Obtém relatório do ano atual
    /// </summary>
    Task<ResultadoDto<RelatorioFinanceiroAnualDto>> ObterRelatorioAnoAtualAsync();

    /// <summary>
    /// Obtém comparativo entre dois meses
    /// </summary>
    Task<ResultadoDto<ComparativoMensalDto>> ObterComparativoMensalAsync(int ano1, int mes1, int ano2, int mes2);
}

/// <summary>
/// DTO para comparativo entre meses
/// </summary>
public class ComparativoMensalDto
{
    /// <summary>
    /// Primeiro período
    /// </summary>
    public PeriodoComparativoDto PrimeiroPeriodo { get; set; } = null!;

    /// <summary>
    /// Segundo período
    /// </summary>
    public PeriodoComparativoDto SegundoPeriodo { get; set; } = null!;

    /// <summary>
    /// Diferença em sessões
    /// </summary>
    public int DiferencaSessoes { get; set; }

    /// <summary>
    /// Diferença em valor faturado
    /// </summary>
    public decimal DiferencaValorFaturado { get; set; }

    /// <summary>
    /// Diferença em valor pago
    /// </summary>
    public decimal DiferencaValorPago { get; set; }

    /// <summary>
    /// Crescimento percentual em faturamento
    /// </summary>
    public decimal CrescimentoPercentualFaturamento { get; set; }

    /// <summary>
    /// Crescimento percentual em pagamentos
    /// </summary>
    public decimal CrescimentoPercentualPagamento { get; set; }
}

/// <summary>
/// DTO para período comparativo
/// </summary>
public class PeriodoComparativoDto
{
    /// <summary>
    /// Ano
    /// </summary>
    public int Ano { get; set; }

    /// <summary>
    /// Mês
    /// </summary>
    public int Mes { get; set; }

    /// <summary>
    /// Nome do período
    /// </summary>
    public string NomePeriodo { get; set; } = string.Empty;

    /// <summary>
    /// Total de sessões realizadas
    /// </summary>
    public int TotalSessoes { get; set; }

    /// <summary>
    /// Valor total faturado
    /// </summary>
    public decimal ValorFaturado { get; set; }

    /// <summary>
    /// Valor total pago
    /// </summary>
    public decimal ValorPago { get; set; }

    /// <summary>
    /// Valor médio por sessão
    /// </summary>
    public decimal ValorMedioSessao { get; set; }
}
