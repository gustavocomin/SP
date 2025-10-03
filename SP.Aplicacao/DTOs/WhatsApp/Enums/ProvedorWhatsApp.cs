namespace SP.Aplicacao.DTOs.WhatsApp.Enums
{
    /// <summary>
    /// Provedor de WhatsApp
    /// </summary>
    public enum ProvedorWhatsApp
    {
        /// <summary>
        /// WhatsApp Business API oficial
        /// </summary>
        WhatsAppBusinessApi = 1,

        /// <summary>
        /// Twilio WhatsApp API
        /// </summary>
        Twilio = 2,

        /// <summary>
        /// ChatAPI (terceiro)
        /// </summary>
        ChatApi = 3,

        /// <summary>
        /// Evolution API (gratuito)
        /// </summary>
        Evolution = 4,

        /// <summary>
        /// Baileys (Node.js)
        /// </summary>
        Baileys = 5
    }
}
