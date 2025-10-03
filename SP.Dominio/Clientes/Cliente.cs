using SP.Dominio.Enums;
using SP.Dominio.Localizacao;

namespace SP.Dominio.Clientes
{
    public class Cliente
    {
        public int Id { get; set; }

        // Dados Pessoais
        public string Nome { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefone { get; set; } = "";
        public string CPF { get; set; } = "";
        public DateTime DataNascimento { get; set; }

        // Endereço
        public int? CidadeId { get; set; }
        public virtual Cidade? Cidade { get; set; }
        public string? CEP { get; set; }
        public string? Endereco { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        public string? Numero { get; set; }

        // Campos legados (manter por compatibilidade)
        public string? Estado { get; set; }
        public string? CidadeNome { get; set; }

        // Campos Financeiros
        public decimal ValorSessao { get; set; }
        public string FormaPagamentoPreferida { get; set; } = "";
        public int? DiaVencimento { get; set; }
        public StatusFinanceiro StatusFinanceiro { get; set; } = StatusFinanceiro.EmDia;

        // Controle e Auditoria
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataUltimaAtualizacao { get; set; }
        public bool Ativo { get; set; } = true;
        public string? Observacoes { get; set; }

        // Campos Específicos para Psicologia
        public string? ContatoEmergencia { get; set; }
        public string? TelefoneEmergencia { get; set; }
        public string? Profissao { get; set; }

        // LGPD
        public bool AceiteLgpd { get; set; }
    }
}