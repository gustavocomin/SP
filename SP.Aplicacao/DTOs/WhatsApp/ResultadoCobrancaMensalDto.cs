namespace SP.Aplicacao.DTOs.WhatsApp
{
    /// <summary>
    /// DTO para resultado do envio em lote da cobrança mensal
    /// </summary>
    public class ResultadoCobrancaMensalDto
    {
        public int TotalClientes { get; set; }
        public int EnviosRealizados { get; set; }
        public int EnviosFalharam { get; set; }
        public List<string> Erros { get; set; } = new();
        public List<CobrancaMensalDto> ClientesProcessados { get; set; } = new();
        public DateTime DataProcessamento { get; set; } = DateTime.Now;
    }
}
