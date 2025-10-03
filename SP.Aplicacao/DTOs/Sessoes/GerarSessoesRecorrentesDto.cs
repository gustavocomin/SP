using SP.Dominio.Enums;

namespace SP.Aplicacao.DTOs.Sessoes;

/// <summary>
/// DTO para geração de sessões recorrentes
/// </summary>
public class GerarSessoesRecorrentesDto
{
    public int ClienteId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public PeriodicidadeSessao Periodicidade { get; set; }
    public TimeSpan HorarioSessao { get; set; }
    public decimal Valor { get; set; }
    public int DuracaoMinutos { get; set; } = 50;
    public string? Observacoes { get; set; }
}
