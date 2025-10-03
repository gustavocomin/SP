namespace SP.Dominio.Enums;

/// <summary>
/// Status da sessão de psicoterapia
/// </summary>
public enum StatusSessao
{
    /// <summary>
    /// Sessão agendada
    /// </summary>
    Agendada = 1,

    /// <summary>
    /// Sessão confirmada pelo cliente
    /// </summary>
    Confirmada = 2,

    /// <summary>
    /// Sessão realizada
    /// </summary>
    Realizada = 3,

    /// <summary>
    /// Sessão cancelada pelo cliente
    /// </summary>
    CanceladaCliente = 4,

    /// <summary>
    /// Sessão cancelada pelo psicólogo
    /// </summary>
    CanceladaPsicologo = 5,

    /// <summary>
    /// Cliente faltou à sessão
    /// </summary>
    Falta = 6,

    /// <summary>
    /// Sessão reagendada
    /// </summary>
    Reagendada = 7
}
