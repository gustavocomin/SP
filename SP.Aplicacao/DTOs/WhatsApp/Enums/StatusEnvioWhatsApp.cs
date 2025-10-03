namespace SP.Aplicacao.DTOs.WhatsApp.Enums
{
    /// <summary>
    /// Status do envio da mensagem WhatsApp
    /// </summary>
    public enum StatusEnvioWhatsApp
    {
        /// <summary>
        /// Mensagem pendente de envio
        /// </summary>
        Pendente = 1,

        /// <summary>
        /// Mensagem enviada com sucesso
        /// </summary>
        Enviado = 2,

        /// <summary>
        /// Mensagem entregue ao destinatário
        /// </summary>
        Entregue = 3,

        /// <summary>
        /// Mensagem lida pelo destinatário
        /// </summary>
        Lido = 4,

        /// <summary>
        /// Erro no envio
        /// </summary>
        Erro = 5,

        /// <summary>
        /// Mensagem rejeitada pelo provedor
        /// </summary>
        Rejeitado = 6
    }
}
