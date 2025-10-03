namespace SP.Aplicacao.DTOs.Financeiro;

/// <summary>
/// DTO para relatório financeiro anual
/// </summary>
public class RelatorioFinanceiroAnualDto
{
    /// <summary>
    /// Ano do relatório
    /// </summary>
    public int Ano { get; set; }

    /// <summary>
    /// Total de sessões realizadas no ano
    /// </summary>
    public int TotalSessoesRealizadas { get; set; }

    /// <summary>
    /// Valor total faturado no ano
    /// </summary>
    public decimal ValorTotalFaturado { get; set; }

    /// <summary>
    /// Valor total pago no ano
    /// </summary>
    public decimal ValorTotalPago { get; set; }

    /// <summary>
    /// Valor total a receber no ano
    /// </summary>
    public decimal ValorTotalAReceber { get; set; }

    /// <summary>
    /// Percentual de pagamento do ano
    /// </summary>
    public decimal PercentualPagamento { get; set; }

    /// <summary>
    /// Valor médio por sessão no ano
    /// </summary>
    public decimal ValorMedioSessao { get; set; }

    /// <summary>
    /// Melhor mês do ano (maior faturamento)
    /// </summary>
    public MelhorMesDto? MelhorMes { get; set; }

    /// <summary>
    /// Relatórios mensais do ano
    /// </summary>
    public List<RelatorioMensalResumoDto> RelatoriosMensais { get; set; } = new();

    /// <summary>
    /// Evolução mensal do faturamento
    /// </summary>
    public List<EvolucaoMensalDto> EvolucaoMensal { get; set; } = new();
}

/// <summary>
/// DTO para o melhor mês do ano
/// </summary>
public class MelhorMesDto
{
    /// <summary>
    /// Mês (1-12)
    /// </summary>
    public int Mes { get; set; }

    /// <summary>
    /// Nome do mês
    /// </summary>
    public string NomeMes { get; set; } = string.Empty;

    /// <summary>
    /// Valor faturado no mês
    /// </summary>
    public decimal ValorFaturado { get; set; }

    /// <summary>
    /// Quantidade de sessões
    /// </summary>
    public int QuantidadeSessoes { get; set; }
}

/// <summary>
/// DTO para resumo mensal
/// </summary>
public class RelatorioMensalResumoDto
{
    /// <summary>
    /// Mês (1-12)
    /// </summary>
    public int Mes { get; set; }

    /// <summary>
    /// Nome do mês
    /// </summary>
    public string NomeMes { get; set; } = string.Empty;

    /// <summary>
    /// Quantidade de sessões realizadas
    /// </summary>
    public int QuantidadeSessoes { get; set; }

    /// <summary>
    /// Valor faturado no mês
    /// </summary>
    public decimal ValorFaturado { get; set; }

    /// <summary>
    /// Valor pago no mês
    /// </summary>
    public decimal ValorPago { get; set; }

    /// <summary>
    /// Valor a receber do mês
    /// </summary>
    public decimal ValorAReceber { get; set; }

    /// <summary>
    /// Percentual de pagamento
    /// </summary>
    public decimal PercentualPagamento { get; set; }
}

/// <summary>
/// DTO para evolução mensal
/// </summary>
public class EvolucaoMensalDto
{
    /// <summary>
    /// Mês (1-12)
    /// </summary>
    public int Mes { get; set; }

    /// <summary>
    /// Nome do mês
    /// </summary>
    public string NomeMes { get; set; } = string.Empty;

    /// <summary>
    /// Valor faturado
    /// </summary>
    public decimal ValorFaturado { get; set; }

    /// <summary>
    /// Crescimento percentual em relação ao mês anterior
    /// </summary>
    public decimal? CrescimentoPercentual { get; set; }

    /// <summary>
    /// Diferença em valor em relação ao mês anterior
    /// </summary>
    public decimal? DiferencaValor { get; set; }
}

/// <summary>
/// DTO para dashboard financeiro
/// </summary>
public class DashboardFinanceiroDto
{
    /// <summary>
    /// Valor total em aberto (todas as sessões não pagas)
    /// </summary>
    public decimal ValorTotalEmAberto { get; set; }

    /// <summary>
    /// Quantidade de sessões não pagas
    /// </summary>
    public int QuantidadeSessoesNaoPagas { get; set; }

    /// <summary>
    /// Faturamento do mês atual
    /// </summary>
    public decimal FaturamentoMesAtual { get; set; }

    /// <summary>
    /// Faturamento do mês anterior
    /// </summary>
    public decimal FaturamentoMesAnterior { get; set; }

    /// <summary>
    /// Crescimento percentual em relação ao mês anterior
    /// </summary>
    public decimal CrescimentoMensal { get; set; }

    /// <summary>
    /// Valor médio por sessão (últimos 3 meses)
    /// </summary>
    public decimal ValorMedioSessao { get; set; }

    /// <summary>
    /// Clientes com maior valor em aberto
    /// </summary>
    public List<ClienteEmAbertoDto> ClientesComMaiorDebito { get; set; } = new();

    /// <summary>
    /// Próximos vencimentos (próximos 7 dias)
    /// </summary>
    public List<ProximoVencimentoDto> ProximosVencimentos { get; set; } = new();
}

/// <summary>
/// DTO para cliente em aberto
/// </summary>
public class ClienteEmAbertoDto
{
    /// <summary>
    /// ID do cliente
    /// </summary>
    public int ClienteId { get; set; }

    /// <summary>
    /// Nome do cliente
    /// </summary>
    public string NomeCliente { get; set; } = string.Empty;

    /// <summary>
    /// Valor total em aberto
    /// </summary>
    public decimal ValorEmAberto { get; set; }

    /// <summary>
    /// Quantidade de sessões não pagas
    /// </summary>
    public int QuantidadeSessoesNaoPagas { get; set; }

    /// <summary>
    /// Sessão mais antiga não paga
    /// </summary>
    public DateTime? SessaoMaisAntigaNaoPaga { get; set; }

    /// <summary>
    /// Dias da sessão mais antiga
    /// </summary>
    public int DiasEmAtraso { get; set; }
}

/// <summary>
/// DTO para próximo vencimento
/// </summary>
public class ProximoVencimentoDto
{
    /// <summary>
    /// ID do cliente
    /// </summary>
    public int ClienteId { get; set; }

    /// <summary>
    /// Nome do cliente
    /// </summary>
    public string NomeCliente { get; set; } = string.Empty;

    /// <summary>
    /// Data de vencimento
    /// </summary>
    public DateTime DataVencimento { get; set; }

    /// <summary>
    /// Valor a vencer
    /// </summary>
    public decimal ValorAVencer { get; set; }

    /// <summary>
    /// Dias até o vencimento
    /// </summary>
    public int DiasAteVencimento { get; set; }
}
