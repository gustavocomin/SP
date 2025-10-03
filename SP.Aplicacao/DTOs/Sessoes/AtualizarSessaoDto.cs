using SP.Dominio.Enums;

namespace SP.Aplicacao.DTOs.Sessoes;

/// <summary>
/// DTO para atualização de sessão
/// </summary>
public class AtualizarSessaoDto
{
    public DateTime DataHoraAgendada { get; set; }
    public int DuracaoMinutos { get; set; }
    public decimal Valor { get; set; }
    public StatusSessao Status { get; set; }
    public PeriodicidadeSessao Periodicidade { get; set; }
    public string? Observacoes { get; set; }
    public string? AnotacoesClinicas { get; set; }
    public string? MotivoCancelamento { get; set; }
    public bool ConsiderarFaturamento { get; set; }
}
