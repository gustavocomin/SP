namespace SP.Aplicacao.DTOs.WhatsApp
{
    /// <summary>
    /// DTO para cobrança mensal personalizada
    /// </summary>
    public class CobrancaMensalDto
    {
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string MesReferencia { get; set; } = string.Empty;
        public int QuantidadeSessoes { get; set; }
        public decimal ValorTotal { get; set; }
        public string ChavePix { get; set; } = "10203773993";
        public DateTime DataVencimento { get; set; }
    }
}
