using SP.Dominio.Enums;

namespace SP.Aplicacao.DTOs.Sessoes;

/// <summary>
/// DTO resumido da sessão para listagens
/// </summary>
public class SessaoResumoDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public DateTime DataHoraAgendada { get; set; }
    public DateTime? DataHoraRealizada { get; set; }
    public int DuracaoMinutos { get; set; }
    public decimal Valor { get; set; }
    public StatusSessao Status { get; set; }
    public string StatusDescricao => Status.ToString();
    public bool Pago { get; set; }
    public bool ConsiderarFaturamento { get; set; }
}
