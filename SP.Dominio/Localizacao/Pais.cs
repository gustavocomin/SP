namespace SP.Dominio.Localizacao;

/// <summary>
/// Entidade que representa um país
/// </summary>
public class Pais
{
    /// <summary>
    /// Identificador único do país
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome do país
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-2 (BR, US, etc.)
    /// </summary>
    public string CodigoISO { get; set; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-3 (BRA, USA, etc.)
    /// </summary>
    public string CodigoISO3 { get; set; } = string.Empty;

    /// <summary>
    /// Código de discagem internacional (+55, +1, etc.)
    /// </summary>
    public string CodigoTelefone { get; set; } = string.Empty;

    /// <summary>
    /// Indica se o país está ativo no sistema
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Data de criação do registro
    /// </summary>
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Estados do país
    /// </summary>
    public virtual ICollection<Estado> Estados { get; set; } = new List<Estado>();
}
