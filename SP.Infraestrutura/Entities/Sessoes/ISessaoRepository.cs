using SP.Dominio.Enums;
using SP.Dominio.Sessoes;
using SP.Infraestrutura.Common.Base;

namespace SP.Infraestrutura.Entities.Sessoes;

/// <summary>
/// Interface do repositório de sessões
/// </summary>
public interface ISessaoRepository : IRepositoryBase<Sessao>
{
    /// <summary>
    /// Obtém todas as sessões de um cliente
    /// </summary>
    Task<List<Sessao>> ObterSessoesPorClienteAsync(int clienteId);

    /// <summary>
    /// Obtém sessões por período
    /// </summary>
    Task<List<Sessao>> ObterSessoesPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);

    /// <summary>
    /// Obtém sessões de um cliente por período
    /// </summary>
    Task<List<Sessao>> ObterSessoesPorClientePeriodoAsync(int clienteId, DateTime dataInicio, DateTime dataFim);

    /// <summary>
    /// Obtém sessões por status
    /// </summary>
    Task<List<Sessao>> ObterSessoesPorStatusAsync(StatusSessao status);

    /// <summary>
    /// Obtém sessões agendadas para hoje
    /// </summary>
    Task<List<Sessao>> ObterSessoesHojeAsync();

    /// <summary>
    /// Obtém sessões agendadas para uma data específica
    /// </summary>
    Task<List<Sessao>> ObterSessoesPorDataAsync(DateTime data);

    /// <summary>
    /// Obtém sessões não pagas
    /// </summary>
    Task<List<Sessao>> ObterSessoesNaoPagasAsync();

    /// <summary>
    /// Obtém sessões não pagas de um cliente
    /// </summary>
    Task<List<Sessao>> ObterSessoesNaoPagasPorClienteAsync(int clienteId);

    /// <summary>
    /// Obtém sessões realizadas no mês para faturamento
    /// </summary>
    Task<List<Sessao>> ObterSessoesParaFaturamentoAsync(int ano, int mes);

    /// <summary>
    /// Obtém sessões realizadas de um cliente no mês
    /// </summary>
    Task<List<Sessao>> ObterSessoesClienteMesAsync(int clienteId, int ano, int mes);

    /// <summary>
    /// Verifica se existe conflito de horário
    /// </summary>
    Task<bool> VerificarConflitoHorarioAsync(DateTime dataHora, int duracaoMinutos, int? sessaoIdExcluir = null);

    /// <summary>
    /// Obtém sessões que conflitam com o horário especificado
    /// </summary>
    Task<List<Sessao>> ObterSessoesConflitantesAsync(DateTime dataHora, int duracaoMinutos, int? sessaoIdExcluir = null);

    /// <summary>
    /// Obtém próximas sessões (próximos 7 dias)
    /// </summary>
    Task<List<Sessao>> ObterProximasSessoesAsync();

    /// <summary>
    /// Conta sessões por status
    /// </summary>
    Task<Dictionary<StatusSessao, int>> ContarSessoesPorStatusAsync();

    /// <summary>
    /// Obtém valor total das sessões realizadas no período
    /// </summary>
    Task<decimal> ObterValorTotalPeriodoAsync(DateTime dataInicio, DateTime dataFim);

    /// <summary>
    /// Obtém valor total das sessões não pagas
    /// </summary>
    Task<decimal> ObterValorTotalNaoPagasAsync();

    /// <summary>
    /// Obtém estatísticas de sessões por cliente
    /// </summary>
    Task<Dictionary<int, int>> ObterEstatisticasSessoesPorClienteAsync(DateTime dataInicio, DateTime dataFim);

    /// <summary>
    /// Marca sessões como pagas em lote
    /// </summary>
    Task MarcarSessoesComoPagasAsync(List<int> sessaoIds, string formaPagamento);

    /// <summary>
    /// Cancela sessões em lote
    /// </summary>
    Task CancelarSessoesAsync(List<int> sessaoIds, string motivo, StatusSessao novoStatus);

    /// <summary>
    /// Obtém sessões que precisam de confirmação (próximas 24h)
    /// </summary>
    Task<List<Sessao>> ObterSessoesParaConfirmacaoAsync();

    /// <summary>
    /// Gera sessões recorrentes baseadas na periodicidade
    /// </summary>
    Task<List<Sessao>> GerarSessoesRecorrentesAsync(int clienteId, DateTime dataInicio, DateTime dataFim, PeriodicidadeSessao periodicidade, TimeSpan horario, decimal valor);

    // Métodos para relatórios financeiros

    /// <summary>
    /// Obtém dados financeiros de um mês específico
    /// </summary>
    Task<(int TotalRealizadas, int TotalCanceladas, int TotalFaltas, decimal ValorFaturado, decimal ValorPago)>
        ObterDadosFinanceirosMensalAsync(int ano, int mes);

    /// <summary>
    /// Obtém sessões realizadas de um mês específico agrupadas por cliente
    /// </summary>
    Task<List<(int ClienteId, string NomeCliente, int QuantidadeSessoes, decimal ValorFaturado, decimal ValorPago, DateTime? UltimaDataPagamento)>>
        ObterSessoesPorClienteMensalAsync(int ano, int mes);

    /// <summary>
    /// Obtém sessões pagas de um mês agrupadas por forma de pagamento
    /// </summary>
    Task<List<(string FormaPagamento, int QuantidadeSessoes, decimal ValorTotal)>>
        ObterSessoesPorFormaPagamentoMensalAsync(int ano, int mes);

    /// <summary>
    /// Obtém sessões não pagas de um mês específico
    /// </summary>
    Task<List<(int Id, string NomeCliente, DateTime DataSessao, decimal Valor)>>
        ObterSessoesNaoPagasMensalAsync(int ano, int mes);

    /// <summary>
    /// Obtém dados financeiros anuais agrupados por mês
    /// </summary>
    Task<List<(int Mes, int QuantidadeSessoes, decimal ValorFaturado, decimal ValorPago)>>
        ObterDadosFinanceirosAnualAsync(int ano);

    /// <summary>
    /// Obtém clientes com maior valor em aberto
    /// </summary>
    Task<List<(int ClienteId, string NomeCliente, decimal ValorEmAberto, int QuantidadeSessoes, DateTime? SessaoMaisAntiga)>>
        ObterClientesComMaiorDebitoAsync(int limite = 10);

    /// <summary>
    /// Obtém próximos vencimentos baseado no dia de vencimento do cliente
    /// </summary>
    Task<List<(int ClienteId, string NomeCliente, DateTime DataVencimento, decimal ValorAVencer)>>
        ObterProximosVencimentosAsync(int diasAntecedencia = 7);
}
