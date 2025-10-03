using SP.Aplicacao.DTOs.WhatsApp.Enums;

namespace SP.Aplicacao.DTOs.WhatsApp
{
    /// <summary>
    /// Configuração geral do WhatsApp
    /// </summary>
    public class ConfiguracaoWhatsAppDto
    {
        /// <summary>
        /// Provedor ativo
        /// </summary>
        public ProvedorWhatsApp ProvedorAtivo { get; set; } = ProvedorWhatsApp.Evolution;

        /// <summary>
        /// Configurações WhatsApp Business API
        /// </summary>
        public ConfiguracaoBusinessApiDto? BusinessApi { get; set; }

        /// <summary>
        /// Configurações de serviços terceiros
        /// </summary>
        public ConfiguracaoTerceirosDto? Terceiros { get; set; }

        /// <summary>
        /// Templates disponíveis
        /// </summary>
        public List<TemplateWhatsAppDto> Templates { get; set; } = [];

        /// <summary>
        /// Ativo/Inativo
        /// </summary>
        public bool Ativo { get; set; } = true;
    }
}
