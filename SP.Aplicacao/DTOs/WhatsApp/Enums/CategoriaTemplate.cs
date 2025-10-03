namespace SP.Aplicacao.DTOs.WhatsApp.Enums
{
    /// <summary>
    /// Categoria do template WhatsApp
    /// </summary>
    public enum CategoriaTemplate
    {
        /// <summary>
        /// Template de marketing
        /// </summary>
        Marketing = 1,

        /// <summary>
        /// Template utilitário (notificações, lembretes)
        /// </summary>
        Utilitario = 2,

        /// <summary>
        /// Template utilitário (alias para compatibilidade)
        /// </summary>
        Utility = 2,

        /// <summary>
        /// Template de autenticação (códigos, verificação)
        /// </summary>
        Autenticacao = 3
    }
}
