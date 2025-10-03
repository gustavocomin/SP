using SP.Aplicacao.DTOs.Clientes;
using SP.Dominio.Enums;

namespace SP.Aplicacao.DTOs.Sessoes;

/// <summary>
/// DTO completo da sessão
/// </summary>
public class SessaoDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public ClienteResumoDto? Cliente { get; set; }
    public DateTime DataHoraAgendada { get; set; }
    public DateTime? DataHoraRealizada { get; set; }
    public int DuracaoMinutos { get; set; }
    public int? DuracaoRealMinutos { get; set; }
    public decimal Valor { get; set; }
    public StatusSessao Status { get; set; }
    public string StatusDescricao => Status.ToString();
    public PeriodicidadeSessao Periodicidade { get; set; }
    public string PeriodicidadeDescricao => Periodicidade.ToString();
    public string? Observacoes { get; set; }
    public string? AnotacoesClinicas { get; set; }
    public string? MotivoCancelamento { get; set; }
    public bool Pago { get; set; }
    public DateTime? DataPagamento { get; set; }
    public string? FormaPagamento { get; set; }
    public bool ConsiderarFaturamento { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataUltimaAtualizacao { get; set; }
    public bool Ativo { get; set; }
    public int? SessaoOriginalId { get; set; }
}
