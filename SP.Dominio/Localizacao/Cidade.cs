namespace SP.Dominio.Localizacao;

/// <summary>
/// Entidade que representa uma cidade/município
/// </summary>
public class Cidade
{
    /// <summary>
    /// Identificador único da cidade
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome da cidade
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código IBGE da cidade
    /// </summary>
    public string? CodigoIBGE { get; set; }

    /// <summary>
    /// CEP principal da cidade (opcional)
    /// </summary>
    public string? CEP { get; set; }

    /// <summary>
    /// Latitude da cidade (para geolocalização)
    /// </summary>
    public decimal? Latitude { get; set; }

    /// <summary>
    /// Longitude da cidade (para geolocalização)
    /// </summary>
    public decimal? Longitude { get; set; }

    /// <summary>
    /// População estimada da cidade
    /// </summary>
    public int? Populacao { get; set; }

    /// <summary>
    /// Área da cidade em km²
    /// </summary>
    public decimal? Area { get; set; }

    /// <summary>
    /// ID do estado ao qual a cidade pertence
    /// </summary>
    public int EstadoId { get; set; }

    /// <summary>
    /// Estado ao qual a cidade pertence
    /// </summary>
    public virtual Estado Estado { get; set; } = null!;

    /// <summary>
    /// Indica se a cidade está ativa no sistema
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Data de criação do registro
    /// </summary>
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
