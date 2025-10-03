namespace SP.Aplicacao.DTOs.Calendario
{
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
}
