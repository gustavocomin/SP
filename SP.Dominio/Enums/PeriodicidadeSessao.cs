namespace SP.Dominio.Enums;

/// <summary>
/// Periodicidade das sessões de psicoterapia
/// </summary>
public enum PeriodicidadeSessao
{
    /// <summary>
    /// Sessões diárias
    /// </summary>
    Diario = 1,

    /// <summary>
    /// Duas vezes por semana
    /// </summary>
    Bisemanal = 2,

    /// <summary>
    /// Uma vez por semana
    /// </summary>
    Semanal = 3,

    /// <summary>
    /// A cada duas semanas
    /// </summary>
    Quinzenal = 4,

    /// <summary>
    /// Uma vez por mês
    /// </summary>
    Mensal = 5,

    /// <summary>
    /// Sem periodicidade fixa - agendamento livre
    /// </summary>
    Livre = 6
}
