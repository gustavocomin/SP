using SP.Dominio.Enums;

namespace SP.Aplicacao.DTOs.Sessoes;

/// <summary>
/// DTO para estatísticas de sessões
/// </summary>
public class EstatisticasSessoesDto
{
    public int TotalSessoes { get; set; }
    public int SessoesAgendadas { get; set; }
    public int SessoesRealizadas { get; set; }
    public int SessoesCanceladas { get; set; }
    public int SessoesHoje { get; set; }
    public int SessoesProximosDias { get; set; }
    public int SessoesNaoPagas { get; set; }
    public decimal ValorTotalRealizado { get; set; }
    public decimal ValorTotalNaoPago { get; set; }
    public decimal ValorMedioSessao { get; set; }
    public Dictionary<StatusSessao, int> SessoesPorStatus { get; set; } = new();
    public Dictionary<int, int> SessoesPorCliente { get; set; } = new();
}
