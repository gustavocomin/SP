using SP.Dominio.Clientes;
using SP.Dominio.Enums;

namespace SP.Dominio.Sessoes;

/// <summary>
/// Entidade que representa uma sessão de psicoterapia
/// </summary>
public class Sessao
{
    /// <summary>
    /// Identificador único da sessão
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID do cliente que participará da sessão
    /// </summary>
    public int ClienteId { get; set; }

    /// <summary>
    /// Cliente que participará da sessão
    /// </summary>
    public Cliente Cliente { get; set; } = null!;

    /// <summary>
    /// Data e hora agendada para a sessão
    /// </summary>
    public DateTime DataHoraAgendada { get; set; }

    /// <summary>
    /// Data e hora real da sessão (quando realizada)
    /// </summary>
    public DateTime? DataHoraRealizada { get; set; }

    /// <summary>
    /// Duração prevista da sessão em minutos
    /// </summary>
    public int DuracaoMinutos { get; set; } = 50;

    /// <summary>
    /// Duração real da sessão em minutos
    /// </summary>
    public int? DuracaoRealMinutos { get; set; }

    /// <summary>
    /// Valor cobrado pela sessão
    /// </summary>
    public decimal Valor { get; set; }

    /// <summary>
    /// Status atual da sessão
    /// </summary>
    public StatusSessao Status { get; set; } = StatusSessao.Agendada;

    /// <summary>
    /// Periodicidade configurada para esta sessão
    /// </summary>
    public PeriodicidadeSessao Periodicidade { get; set; } = PeriodicidadeSessao.Semanal;

    /// <summary>
    /// Observações sobre a sessão
    /// </summary>
    public string? Observacoes { get; set; }

    /// <summary>
    /// Anotações clínicas da sessão (privadas do psicólogo)
    /// </summary>
    public string? AnotacoesClinicas { get; set; }

    /// <summary>
    /// Motivo do cancelamento (se aplicável)
    /// </summary>
    public string? MotivoCancelamento { get; set; }

    /// <summary>
    /// Indica se a sessão foi paga
    /// </summary>
    public bool Pago { get; set; } = false;

    /// <summary>
    /// Data do pagamento
    /// </summary>
    public DateTime? DataPagamento { get; set; }

    /// <summary>
    /// Forma de pagamento utilizada
    /// </summary>
    public string? FormaPagamento { get; set; }

    /// <summary>
    /// Indica se a sessão deve ser considerada para faturamento
    /// </summary>
    public bool ConsiderarFaturamento { get; set; } = true;

    /// <summary>
    /// Data de criação do registro
    /// </summary>
    public DateTime DataCriacao { get; set; } = DateTime.Now;

    /// <summary>
    /// Data da última atualização
    /// </summary>
    public DateTime? DataUltimaAtualizacao { get; set; }

    /// <summary>
    /// Indica se o registro está ativo (soft delete)
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// ID da sessão original (para reagendamentos)
    /// </summary>
    public int? SessaoOriginalId { get; set; }

    /// <summary>
    /// Sessão original (para reagendamentos)
    /// </summary>
    public Sessao? SessaoOriginal { get; set; }

    /// <summary>
    /// Sessões reagendadas a partir desta
    /// </summary>
    public ICollection<Sessao> SessoesReagendadas { get; set; } = new List<Sessao>();
}
