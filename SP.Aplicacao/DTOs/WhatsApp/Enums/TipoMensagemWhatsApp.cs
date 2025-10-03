namespace SP.Aplicacao.DTOs.WhatsApp.Enums
{
    /// <summary>
    /// Tipo de mensagem WhatsApp
    /// </summary>
    public enum TipoMensagemWhatsApp
    {
        /// <summary>
        /// Mensagem de texto simples
        /// </summary>
        Texto = 1,

        /// <summary>
        /// Template pré-aprovado (WhatsApp Business API)
        /// </summary>
        Template = 2,

        /// <summary>
        /// Mensagem com imagem
        /// </summary>
        Imagem = 3,

        /// <summary>
        /// Mensagem com documento
        /// </summary>
        Documento = 4,

        /// <summary>
        /// Mensagem com vídeo
        /// </summary>
        Video = 5,

        /// <summary>
        /// Mensagem com áudio
        /// </summary>
        Audio = 6,

        /// <summary>
        /// Mensagem com mídia (genérico)
        /// </summary>
        Midia = 7,

        /// <summary>
        /// Mensagem interativa (botões, lista)
        /// </summary>
        Interativa = 8,

        /// <summary>
        /// Localização
        /// </summary>
        Localizacao = 9
    }
}
