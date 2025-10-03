using SP.Dominio.Enums;

namespace SP.Aplicacao.DTOs.Sessoes;

/// <summary>
/// DTO para criação de sessão
/// </summary>
public class CriarSessaoDto
{
    public int ClienteId { get; set; }
    public DateTime DataHoraAgendada { get; set; }
    public int DuracaoMinutos { get; set; } = 50;
    public decimal Valor { get; set; }
    public PeriodicidadeSessao Periodicidade { get; set; } = PeriodicidadeSessao.Semanal;
    public string? Observacoes { get; set; }
    public bool ConsiderarFaturamento { get; set; } = true;
}
