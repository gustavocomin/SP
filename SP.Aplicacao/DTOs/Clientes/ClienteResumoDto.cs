using SP.Dominio.Enums;

namespace SP.Aplicacao.DTOs.Clientes
{
    public class ClienteResumoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public decimal ValorSessao { get; set; }
        public StatusFinanceiro StatusFinanceiro { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}
