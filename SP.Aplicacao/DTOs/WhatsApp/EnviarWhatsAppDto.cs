using SP.Aplicacao.DTOs.WhatsApp.Enums;

namespace SP.Aplicacao.DTOs.WhatsApp
{
    /// <summary>
    /// DTO para envio de mensagem WhatsApp
    /// </summary>
    public class EnviarWhatsAppDto
    {
        /// <summary>
        /// Número do destinatário (formato: 5511999999999)
        /// </summary>
        public string Telefone { get; set; } = string.Empty;

        /// <summary>
        /// Mensagem a ser enviada
        /// </summary>
        public string Mensagem { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de mensagem (texto, template, midia)
        /// </summary>
        public TipoMensagemWhatsApp Tipo { get; set; } = TipoMensagemWhatsApp.Texto;

        /// <summary>
        /// Template a ser usado (para WhatsApp Business API)
        /// </summary>
        public string? Template { get; set; }

        /// <summary>
        /// Parâmetros do template
        /// </summary>
        public List<string> ParametrosTemplate { get; set; } = [];

        /// <summary>
        /// URL da mídia (para mensagens de mídia)
        /// </summary>
        public string? UrlMidia { get; set; }

        /// <summary>
        /// Tipo da mídia (image, document, video, audio)
        /// </summary>
        public string? TipoMidia { get; set; }

        /// <summary>
        /// Agendar envio para data/hora específica
        /// </summary>
        public DateTime? DataAgendamento { get; set; }

        /// <summary>
        /// ID da sessão relacionada (opcional)
        /// </summary>
        public int? SessaoId { get; set; }

        /// <summary>
        /// ID do cliente relacionado (opcional)
        /// </summary>
        public int? ClienteId { get; set; }
    }
}
