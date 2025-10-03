namespace SP.Aplicacao.DTOs.Calendario
{
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
}
