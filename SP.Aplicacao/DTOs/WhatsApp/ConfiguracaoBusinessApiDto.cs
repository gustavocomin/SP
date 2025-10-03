namespace SP.Aplicacao.DTOs.WhatsApp
{
    /// <summary>
    /// Configuração WhatsApp Business API
    /// </summary>
    public class ConfiguracaoBusinessApiDto
    {
        /// <summary>
        /// Token de acesso
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// ID do número de telefone
        /// </summary>
        public string? PhoneNumberId { get; set; }

        /// <summary>
        /// ID da conta WhatsApp Business
        /// </summary>
        public string? WhatsAppBusinessAccountId { get; set; }

        /// <summary>
        /// URL da API
        /// </summary>
        public string ApiUrl { get; set; } = "https://graph.facebook.com/v18.0";

        /// <summary>
        /// URL do webhook
        /// </summary>
        public string? WebhookUrl { get; set; }

        /// <summary>
        /// Token de verificação do webhook
        /// </summary>
        public string? WebhookVerifyToken { get; set; }
    }
}
