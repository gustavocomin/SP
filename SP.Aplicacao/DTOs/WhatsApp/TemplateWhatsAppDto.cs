using SP.Aplicacao.DTOs.WhatsApp.Enums;

namespace SP.Aplicacao.DTOs.WhatsApp
{
    /// <summary>
    /// Template de mensagem WhatsApp
    /// </summary>
    public class TemplateWhatsAppDto
    {
        /// <summary>
        /// Nome do template
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Categoria do template
        /// </summary>
        public CategoriaTemplate Categoria { get; set; }

        /// <summary>
        /// Idioma do template
        /// </summary>
        public string Idioma { get; set; } = "pt_BR";

        /// <summary>
        /// Conteúdo do template
        /// </summary>
        public string Conteudo { get; set; } = string.Empty;

        /// <summary>
        /// Parâmetros do template
        /// </summary>
        public List<string> Parametros { get; set; } = [];

        /// <summary>
        /// Status do template
        /// </summary>
        public StatusTemplate Status { get; set; }

        /// <summary>
        /// Ativo/Inativo
        /// </summary>
        public bool Ativo { get; set; } = true;
    }
}
