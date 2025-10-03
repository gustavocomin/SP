using SP.Aplicacao.DTOs.Clientes;

namespace SP.Aplicacao.DTOs.Financeiro;

/// <summary>
/// DTO para relatório financeiro mensal
/// </summary>
public class RelatorioFinanceiroMensalDto
{
    /// <summary>
    /// Ano do relatório
    /// </summary>
    public int Ano { get; set; }

    /// <summary>
    /// Mês do relatório (1-12)
    /// </summary>
    public int Mes { get; set; }

    /// <summary>
    /// Nome do mês
    /// </summary>
    public string NomeMes { get; set; } = string.Empty;

    /// <summary>
    /// Total de sessões realizadas no mês
    /// </summary>
    public int TotalSessoesRealizadas { get; set; }

    /// <summary>
    /// Total de sessões canceladas no mês
    /// </summary>
    public int TotalSessoesCanceladas { get; set; }

    /// <summary>
    /// Total de faltas no mês
    /// </summary>
    public int TotalFaltas { get; set; }

    /// <summary>
    /// Valor total faturado no mês (sessões realizadas)
    /// </summary>
    public decimal ValorTotalFaturado { get; set; }

    /// <summary>
    /// Valor total pago no mês
    /// </summary>
    public decimal ValorTotalPago { get; set; }

    /// <summary>
    /// Valor total a receber (faturado mas não pago)
    /// </summary>
    public decimal ValorTotalAReceber { get; set; }

    /// <summary>
    /// Percentual de pagamento do mês
    /// </summary>
    public decimal PercentualPagamento { get; set; }

    /// <summary>
    /// Valor médio por sessão no mês
    /// </summary>
    public decimal ValorMedioSessao { get; set; }

    /// <summary>
    /// Detalhamento por cliente
    /// </summary>
    public List<RelatorioClienteMensalDto> DetalhesPorCliente { get; set; } = new();

    /// <summary>
    /// Detalhamento por forma de pagamento
    /// </summary>
    public List<RelatorioFormaPagamentoDto> DetalhesPorFormaPagamento { get; set; } = new();

    /// <summary>
    /// Sessões não pagas do mês
    /// </summary>
    public List<SessaoNaoPagaDto> SessoesNaoPagas { get; set; } = new();
}

/// <summary>
/// DTO para detalhamento financeiro por cliente no mês
/// </summary>
public class RelatorioClienteMensalDto
{
    /// <summary>
    /// Dados do cliente
    /// </summary>
    public ClienteResumoDto Cliente { get; set; } = null!;

    /// <summary>
    /// Quantidade de sessões realizadas
    /// </summary>
    public int QuantidadeSessoes { get; set; }

    /// <summary>
    /// Valor total faturado para o cliente
    /// </summary>
    public decimal ValorTotalFaturado { get; set; }

    /// <summary>
    /// Valor total pago pelo cliente
    /// </summary>
    public decimal ValorTotalPago { get; set; }

    /// <summary>
    /// Valor em aberto do cliente
    /// </summary>
    public decimal ValorEmAberto { get; set; }

    /// <summary>
    /// Percentual de pagamento do cliente
    /// </summary>
    public decimal PercentualPagamento { get; set; }

    /// <summary>
    /// Última data de pagamento
    /// </summary>
    public DateTime? UltimaDataPagamento { get; set; }
}

/// <summary>
/// DTO para detalhamento por forma de pagamento
/// </summary>
public class RelatorioFormaPagamentoDto
{
    /// <summary>
    /// Forma de pagamento
    /// </summary>
    public string FormaPagamento { get; set; } = string.Empty;

    /// <summary>
    /// Quantidade de sessões pagas com esta forma
    /// </summary>
    public int QuantidadeSessoes { get; set; }

    /// <summary>
    /// Valor total recebido nesta forma de pagamento
    /// </summary>
    public decimal ValorTotal { get; set; }

    /// <summary>
    /// Percentual do total recebido
    /// </summary>
    public decimal PercentualDoTotal { get; set; }
}

/// <summary>
/// DTO para sessão não paga
/// </summary>
public class SessaoNaoPagaDto
{
    /// <summary>
    /// ID da sessão
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome do cliente
    /// </summary>
    public string NomeCliente { get; set; } = string.Empty;

    /// <summary>
    /// Data da sessão
    /// </summary>
    public DateTime DataSessao { get; set; }

    /// <summary>
    /// Valor da sessão
    /// </summary>
    public decimal Valor { get; set; }

    /// <summary>
    /// Dias em atraso
    /// </summary>
    public int DiasEmAtraso { get; set; }
}
