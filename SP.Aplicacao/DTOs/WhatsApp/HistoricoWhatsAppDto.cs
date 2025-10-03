using SP.Aplicacao.DTOs.WhatsApp.Enums;

namespace SP.Aplicacao.DTOs.WhatsApp
{
    /// <summary>
    /// Histórico de mensagens WhatsApp
    /// </summary>
    public class HistoricoWhatsAppDto
    {
        /// <summary>
        /// ID único do histórico
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID da mensagem no provedor
        /// </summary>
        public string? MessageId { get; set; }

        /// <summary>
        /// Número do destinatário
        /// </summary>
        public string Telefone { get; set; } = string.Empty;

        /// <summary>
        /// Mensagem enviada
        /// </summary>
        public string Mensagem { get; set; } = string.Empty;

        /// <summary>
        /// Status do envio
        /// </summary>
        public StatusEnvioWhatsApp Status { get; set; }

        /// <summary>
        /// Data/hora do envio
        /// </summary>
        public DateTime DataEnvio { get; set; }

        /// <summary>
        /// Provedor usado
        /// </summary>
        public ProvedorWhatsApp Provedor { get; set; }

        /// <summary>
        /// ID da mensagem no provedor
        /// </summary>
        public string? ProvedorMessageId { get; set; }

        /// <summary>
        /// Cliente relacionado
        /// </summary>
        public int? ClienteId { get; set; }

        /// <summary>
        /// Sessão relacionada
        /// </summary>
        public int? SessaoId { get; set; }

        /// <summary>
        /// Custo do envio
        /// </summary>
        public decimal? Custo { get; set; }
    }
}
