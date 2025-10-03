namespace SP.Dominio.Localizacao;

/// <summary>
/// Entidade que representa um estado/província
/// </summary>
public class Estado
{
    /// <summary>
    /// Identificador único do estado
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome do estado
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Sigla do estado (SP, RJ, MG, etc.)
    /// </summary>
    public string Sigla { get; set; } = string.Empty;

    /// <summary>
    /// Código IBGE do estado
    /// </summary>
    public string? CodigoIBGE { get; set; }

    /// <summary>
    /// Região do estado (Norte, Nordeste, etc.)
    /// </summary>
    public string? Regiao { get; set; }

    /// <summary>
    /// ID do país ao qual o estado pertence
    /// </summary>
    public int PaisId { get; set; }

    /// <summary>
    /// País ao qual o estado pertence
    /// </summary>
    public virtual Pais Pais { get; set; } = null!;

    /// <summary>
    /// Indica se o estado está ativo no sistema
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Data de criação do registro
    /// </summary>
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Cidades do estado
    /// </summary>
    public virtual ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();
}
