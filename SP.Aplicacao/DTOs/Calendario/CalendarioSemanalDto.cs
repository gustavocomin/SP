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

/// <summary>
/// DTO para um dia específico do calendário
/// </summary>
public class CalendarioDiaDto
{
    /// <summary>
    /// Data do dia
    /// </summary>
    public DateTime Data { get; set; }

    /// <summary>
    /// Nome do dia da semana
    /// </summary>
    public string DiaSemana { get; set; } = string.Empty;

    /// <summary>
    /// Dia do mês
    /// </summary>
    public int DiaMes { get; set; }

    /// <summary>
    /// Indica se é hoje
    /// </summary>
    public bool EhHoje { get; set; }

    /// <summary>
    /// Indica se é fim de semana
    /// </summary>
    public bool EhFimDeSemana { get; set; }

    /// <summary>
    /// Sessões agendadas para o dia
    /// </summary>
    public List<CalendarioSessaoDto> Sessoes { get; set; } = new();

    /// <summary>
    /// Horários livres disponíveis
    /// </summary>
    public List<CalendarioHorarioLivreDto> HorariosLivres { get; set; } = new();

    /// <summary>
    /// Resumo do dia
    /// </summary>
    public CalendarioResumoDiaDto Resumo { get; set; } = new();
}

/// <summary>
/// DTO para sessão no calendário
/// </summary>
public class CalendarioSessaoDto
{
    /// <summary>
    /// ID da sessão
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID do cliente
    /// </summary>
    public int ClienteId { get; set; }

    /// <summary>
    /// Nome do cliente
    /// </summary>
    public string NomeCliente { get; set; } = string.Empty;

    /// <summary>
    /// Telefone do cliente
    /// </summary>
    public string? TelefoneCliente { get; set; }

    /// <summary>
    /// Data e hora da sessão
    /// </summary>
    public DateTime DataHora { get; set; }

    /// <summary>
    /// Hora formatada (HH:mm)
    /// </summary>
    public string Hora { get; set; } = string.Empty;

    /// <summary>
    /// Duração em minutos
    /// </summary>
    public int DuracaoMinutos { get; set; }

    /// <summary>
    /// Hora de término
    /// </summary>
    public string HoraFim { get; set; } = string.Empty;

    /// <summary>
    /// Status da sessão
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Cor para exibição no calendário
    /// </summary>
    public string Cor { get; set; } = string.Empty;

    /// <summary>
    /// Valor da sessão
    /// </summary>
    public decimal Valor { get; set; }

    /// <summary>
    /// Observações
    /// </summary>
    public string? Observacoes { get; set; }

    /// <summary>
    /// Indica se a sessão foi paga
    /// </summary>
    public bool Pago { get; set; }

    /// <summary>
    /// ID do evento no Google Calendar (se sincronizado)
    /// </summary>
    public string? GoogleCalendarEventId { get; set; }

    /// <summary>
    /// Indica se está sincronizado com Google Calendar
    /// </summary>
    public bool SincronizadoGoogle { get; set; }
}

/// <summary>
/// DTO para horário livre no calendário
/// </summary>
public class CalendarioHorarioLivreDto
{
    /// <summary>
    /// Hora de início
    /// </summary>
    public string HoraInicio { get; set; } = string.Empty;

    /// <summary>
    /// Hora de fim
    /// </summary>
    public string HoraFim { get; set; } = string.Empty;

    /// <summary>
    /// Duração em minutos
    /// </summary>
    public int DuracaoMinutos { get; set; }

    /// <summary>
    /// Indica se é um horário preferencial
    /// </summary>
    public bool HorarioPreferencial { get; set; }
}

/// <summary>
/// DTO para resumo do dia
/// </summary>
public class CalendarioResumoDiaDto
{
    /// <summary>
    /// Total de sessões agendadas
    /// </summary>
    public int TotalSessoes { get; set; }

    /// <summary>
    /// Sessões confirmadas
    /// </summary>
    public int SessoesConfirmadas { get; set; }

    /// <summary>
    /// Sessões pendentes
    /// </summary>
    public int SessoesPendentes { get; set; }

    /// <summary>
    /// Valor total do dia
    /// </summary>
    public decimal ValorTotal { get; set; }

    /// <summary>
    /// Primeira sessão do dia
    /// </summary>
    public string? PrimeiraSessao { get; set; }

    /// <summary>
    /// Última sessão do dia
    /// </summary>
    public string? UltimaSessao { get; set; }

    /// <summary>
    /// Horários livres disponíveis
    /// </summary>
    public int HorariosLivres { get; set; }
}

/// <summary>
/// DTO para resumo semanal
/// </summary>
public class CalendarioResumoSemanalDto
{
    /// <summary>
    /// Total de sessões na semana
    /// </summary>
    public int TotalSessoes { get; set; }

    /// <summary>
    /// Valor total da semana
    /// </summary>
    public decimal ValorTotal { get; set; }

    /// <summary>
    /// Dia com mais sessões
    /// </summary>
    public string? DiaMaisSessoes { get; set; }

    /// <summary>
    /// Quantidade de sessões no dia mais ocupado
    /// </summary>
    public int MaxSessoesDia { get; set; }

    /// <summary>
    /// Média de sessões por dia
    /// </summary>
    public decimal MediaSessoesDia { get; set; }

    /// <summary>
    /// Total de horários livres na semana
    /// </summary>
    public int TotalHorariosLivres { get; set; }
}
