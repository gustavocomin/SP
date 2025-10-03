namespace SP.Aplicacao.DTOs.Calendario
{
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
}
