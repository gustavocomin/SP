using SP.Dominio.Enums;

namespace SP.Aplicacao.DTOs.Clientes
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        
        // Endereço
        public string Estado { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string? Complemento { get; set; }
        
        // Financeiro
        public decimal ValorSessao { get; set; }
        public string FormaPagamentoPreferida { get; set; } = string.Empty;
        public int DiaVencimento { get; set; }
        public StatusFinanceiro StatusFinanceiro { get; set; }
        
        // Contato de Emergência
        public string? ContatoEmergencia { get; set; }
        public string? TelefoneEmergencia { get; set; }
        
        // Informações Adicionais
        public string? Profissao { get; set; }
        
        // LGPD
        public bool AceiteLgpd { get; set; }
        
        // Controle
        public DateTime DataCadastro { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public bool Ativo { get; set; }
        public string? Observacoes { get; set; }
    }
}
