namespace SP.Aplicacao.DTOs.Calendario
{
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
        /// Indica se é uma sessão recorrente
        /// </summary>
        public bool EhRecorrente { get; set; }

        /// <summary>
        /// ID do evento no Google Calendar (se integrado)
        /// </summary>
        public string? GoogleCalendarEventId { get; set; }

        /// <summary>
        /// Indica se a sessão foi paga
        /// </summary>
        public bool Pago { get; set; }

        /// <summary>
        /// Indica se está sincronizado com Google Calendar
        /// </summary>
        public bool SincronizadoGoogle { get; set; }
    }
}
