namespace SP.Aplicacao.DTOs.Calendario;

/// <summary>
/// DTO para configuração do Google Calendar
/// </summary>
public class GoogleCalendarConfigDto
{
    /// <summary>
    /// ID do calendário no Google
    /// </summary>
    public string CalendarId { get; set; } = string.Empty;

    /// <summary>
    /// Nome do calendário
    /// </summary>
    public string CalendarName { get; set; } = string.Empty;

    /// <summary>
    /// Indica se a sincronização está ativa
    /// </summary>
    public bool SincronizacaoAtiva { get; set; }

    /// <summary>
    /// Última sincronização
    /// </summary>
    public DateTime? UltimaSincronizacao { get; set; }

    /// <summary>
    /// Token de acesso
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// Token de refresh
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Data de expiração do token
    /// </summary>
    public DateTime? TokenExpiration { get; set; }
}

/// <summary>
/// DTO para evento do Google Calendar
/// </summary>
public class GoogleCalendarEventDto
{
    /// <summary>
    /// ID do evento no Google Calendar
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Título do evento
    /// </summary>
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// Descrição do evento
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Data e hora de início
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Data e hora de fim
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Localização
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Cor do evento
    /// </summary>
    public string? ColorId { get; set; }

    /// <summary>
    /// Status do evento
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Participantes
    /// </summary>
    public List<GoogleCalendarAttendeeDto> Attendees { get; set; } = new();

    /// <summary>
    /// Lembretes
    /// </summary>
    public List<GoogleCalendarReminderDto> Reminders { get; set; } = new();

    /// <summary>
    /// ID da sessão no sistema local
    /// </summary>
    public int? SessaoId { get; set; }
}

/// <summary>
/// DTO para participante do evento
/// </summary>
public class GoogleCalendarAttendeeDto
{
    /// <summary>
    /// Email do participante
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Nome do participante
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Status de resposta
    /// </summary>
    public string ResponseStatus { get; set; } = string.Empty;

    /// <summary>
    /// Indica se é organizador
    /// </summary>
    public bool Organizer { get; set; }
}

/// <summary>
/// DTO para lembrete do evento
/// </summary>
public class GoogleCalendarReminderDto
{
    /// <summary>
    /// Método do lembrete (email, popup)
    /// </summary>
    public string Method { get; set; } = string.Empty;

    /// <summary>
    /// Minutos antes do evento
    /// </summary>
    public int Minutes { get; set; }
}

/// <summary>
/// DTO para sincronização com Google Calendar
/// </summary>
public class SincronizarGoogleCalendarDto
{
    /// <summary>
    /// Data de início para sincronização
    /// </summary>
    public DateTime DataInicio { get; set; }

    /// <summary>
    /// Data de fim para sincronização
    /// </summary>
    public DateTime DataFim { get; set; }

    /// <summary>
    /// Sincronizar apenas sessões não sincronizadas
    /// </summary>
    public bool ApenasNaoSincronizadas { get; set; } = true;

    /// <summary>
    /// Sobrescrever eventos existentes
    /// </summary>
    public bool SobrescreverExistentes { get; set; } = false;

    /// <summary>
    /// Incluir lembretes automáticos
    /// </summary>
    public bool IncluirLembretes { get; set; } = true;

    /// <summary>
    /// Minutos antes para lembrete por email
    /// </summary>
    public int LembreteEmailMinutos { get; set; } = 1440; // 24 horas

    /// <summary>
    /// Minutos antes para lembrete popup
    /// </summary>
    public int LembretePopupMinutos { get; set; } = 15;
}

/// <summary>
/// DTO para resultado da sincronização
/// </summary>
public class ResultadoSincronizacaoDto
{
    /// <summary>
    /// Indica se a sincronização foi bem-sucedida
    /// </summary>
    public bool Sucesso { get; set; }

    /// <summary>
    /// Mensagem de resultado
    /// </summary>
    public string Mensagem { get; set; } = string.Empty;

    /// <summary>
    /// Eventos criados
    /// </summary>
    public int EventosCriados { get; set; }

    /// <summary>
    /// Eventos atualizados
    /// </summary>
    public int EventosAtualizados { get; set; }

    /// <summary>
    /// Eventos removidos
    /// </summary>
    public int EventosRemovidos { get; set; }

    /// <summary>
    /// Erros ocorridos
    /// </summary>
    public List<string> Erros { get; set; } = new();

    /// <summary>
    /// Data e hora da sincronização
    /// </summary>
    public DateTime DataSincronizacao { get; set; } = DateTime.Now;

    /// <summary>
    /// Tempo total da sincronização
    /// </summary>
    public TimeSpan TempoSincronizacao { get; set; }
}

/// <summary>
/// DTO para configurações de horário de trabalho
/// </summary>
public class HorarioTrabalhoDto
{
    /// <summary>
    /// Dia da semana (0 = Domingo, 1 = Segunda, etc.)
    /// </summary>
    public int DiaSemana { get; set; }

    /// <summary>
    /// Nome do dia da semana
    /// </summary>
    public string NomeDia { get; set; } = string.Empty;

    /// <summary>
    /// Indica se trabalha neste dia
    /// </summary>
    public bool Trabalha { get; set; }

    /// <summary>
    /// Hora de início do trabalho
    /// </summary>
    public TimeSpan? HoraInicio { get; set; }

    /// <summary>
    /// Hora de fim do trabalho
    /// </summary>
    public TimeSpan? HoraFim { get; set; }

    /// <summary>
    /// Hora de início do almoço
    /// </summary>
    public TimeSpan? HoraInicioAlmoco { get; set; }

    /// <summary>
    /// Hora de fim do almoço
    /// </summary>
    public TimeSpan? HoraFimAlmoco { get; set; }

    /// <summary>
    /// Duração padrão das sessões em minutos
    /// </summary>
    public int DuracaoSessaoMinutos { get; set; } = 50;

    /// <summary>
    /// Intervalo entre sessões em minutos
    /// </summary>
    public int IntervaloSessoesMinutos { get; set; } = 10;
}
