namespace SP.Aplicacao.DTOs.Calendario;

/// <summary>
/// DTO para visualização semanal do calendário
/// </summary>
public class CalendarioSemanalDto
{
    /// <summary>
    /// Data de início da semana (segunda-feira)
    /// </summary>
    public DateTime DataInicioSemana { get; set; }

    /// <summary>
    /// Data de fim da semana (domingo)
    /// </summary>
    public DateTime DataFimSemana { get; set; }

    /// <summary>
    /// Número da semana no ano
    /// </summary>
    public int NumeroSemana { get; set; }

    /// <summary>
    /// Mês e ano da semana
    /// </summary>
    public string MesAno { get; set; } = string.Empty;

    /// <summary>
    /// Dias da semana com suas sessões
    /// </summary>
    public List<CalendarioDiaDto> Dias { get; set; } = new();

    /// <summary>
    /// Resumo da semana
    /// </summary>
    public CalendarioResumoSemanalDto Resumo { get; set; } = new();
}

// Classe CalendarioDiaDto movida para arquivo separado

// Classe CalendarioSessaoDto movida para arquivo separado

// Classes movidas para arquivos separados:
// - CalendarioHorarioLivreDto.cs
// - CalendarioResumoDiaDto.cs
// - CalendarioResumoSemanalDto.cs
