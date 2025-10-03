namespace SP.Aplicacao.DTOs.WhatsApp
{
    /// <summary>
    /// Configuração de serviços terceiros
    /// </summary>
    public class ConfiguracaoTerceirosDto
    {
        /// <summary>
        /// Serviço ativo (Twilio, Evolution, ChatAPI, etc.)
        /// </summary>
        public string ServicoAtivo { get; set; } = "Evolution";

        /// <summary>
        /// Chave da API
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// URL da API
        /// </summary>
        public string? ApiUrl { get; set; }

        /// <summary>
        /// Configurações específicas do serviço
        /// </summary>
        public Dictionary<string, object> ConfiguracoesEspecificas { get; set; } = new();
    }
}
