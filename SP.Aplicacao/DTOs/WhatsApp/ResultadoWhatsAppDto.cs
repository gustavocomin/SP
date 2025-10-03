using SP.Aplicacao.DTOs.WhatsApp.Enums;

namespace SP.Aplicacao.DTOs.WhatsApp
{
    /// <summary>
    /// DTO para resultado do envio WhatsApp
    /// </summary>
    public class ResultadoWhatsAppDto
    {
        /// <summary>
        /// ID da mensagem no provedor
        /// </summary>
        public string? MessageId { get; set; }

        /// <summary>
        /// Status do envio
        /// </summary>
        public StatusEnvioWhatsApp Status { get; set; }

        /// <summary>
        /// Mensagem de erro (se houver)
        /// </summary>
        public string? MensagemErro { get; set; }

        /// <summary>
        /// Data/hora do envio
        /// </summary>
        public DateTime DataEnvio { get; set; }

        /// <summary>
        /// Provedor usado para o envio
        /// </summary>
        public ProvedorWhatsApp Provedor { get; set; }

        /// <summary>
        /// Custo do envio
        /// </summary>
        public decimal? Custo { get; set; }

        /// <summary>
        /// Dados específicos do provedor
        /// </summary>
        public Dictionary<string, object> DadosProvedor { get; set; } = new();
    }
}
