namespace SP.Aplicacao.DTOs.Calendario
{
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
}
