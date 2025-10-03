namespace SP.Aplicacao.DTOs.WhatsApp
{
    /// <summary>
    /// Estatísticas de WhatsApp
    /// </summary>
    public class EstatisticasWhatsAppDto
    {
        /// <summary>
        /// Período das estatísticas
        /// </summary>
        public string Periodo { get; set; } = string.Empty;

        /// <summary>
        /// Total de mensagens enviadas
        /// </summary>
        public int TotalEnviadas { get; set; }

        /// <summary>
        /// Total de mensagens entregues
        /// </summary>
        public int TotalEntregues { get; set; }

        /// <summary>
        /// Total de mensagens lidas
        /// </summary>
        public int TotalLidas { get; set; }

        /// <summary>
        /// Total de mensagens com erro
        /// </summary>
        public int TotalErros { get; set; }

        /// <summary>
        /// Taxa de entrega (%)
        /// </summary>
        public decimal TaxaEntrega { get; set; }

        /// <summary>
        /// Taxa de leitura (%)
        /// </summary>
        public decimal TaxaLeitura { get; set; }

        /// <summary>
        /// Custo total
        /// </summary>
        public decimal CustoTotal { get; set; }

        /// <summary>
        /// Custo médio por mensagem
        /// </summary>
        public decimal CustoMedio { get; set; }

        /// <summary>
        /// Distribuição por provedor
        /// </summary>
        public Dictionary<string, int> DistribuicaoProvedor { get; set; } = new();

        /// <summary>
        /// Distribuição por status
        /// </summary>
        public Dictionary<string, int> DistribuicaoStatus { get; set; } = new();
    }
}
