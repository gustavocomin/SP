using FluentValidation;
using SP.Aplicacao.DTOs.Clientes;

namespace SP.Aplicacao.Validators.Clientes
{
    public class CriarClienteValidator : AbstractValidator<CriarClienteDto>
    {
        public CriarClienteValidator()
        {
            // Nome
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres");

            // Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório")
                .EmailAddress().WithMessage("Email deve ter um formato válido")
                .MaximumLength(100).WithMessage("Email deve ter no máximo 100 caracteres");

            // Telefone
            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório")
                .Matches(@"^\(\d{2}\)\s\d{4,5}-\d{4}$")
                .WithMessage("Telefone deve estar no formato (XX) XXXXX-XXXX");

            // CPF
            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("CPF é obrigatório")
                .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$")
                .WithMessage("CPF deve estar no formato XXX.XXX.XXX-XX")
                .Must(ValidarCPF).WithMessage("CPF inválido");

            // Data de Nascimento
            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória")
                .LessThan(DateTime.Today).WithMessage("Data de nascimento deve ser anterior à data atual")
                .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage("Data de nascimento inválida");

            // Endereço
            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("Estado é obrigatório")
                .Length(2).WithMessage("Estado deve ter 2 caracteres");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("Cidade é obrigatória")
                .Length(2, 50).WithMessage("Cidade deve ter entre 2 e 50 caracteres");

            RuleFor(x => x.CEP)
                .NotEmpty().WithMessage("CEP é obrigatório")
                .Matches(@"^\d{5}-\d{3}$").WithMessage("CEP deve estar no formato XXXXX-XXX");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("Endereço é obrigatório")
                .Length(5, 200).WithMessage("Endereço deve ter entre 5 e 200 caracteres");

            RuleFor(x => x.Bairro)
                .NotEmpty().WithMessage("Bairro é obrigatório")
                .Length(2, 50).WithMessage("Bairro deve ter entre 2 e 50 caracteres");

            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("Número é obrigatório")
                .Length(1, 10).WithMessage("Número deve ter entre 1 e 10 caracteres");

            RuleFor(x => x.Complemento)
                .MaximumLength(100).WithMessage("Complemento deve ter no máximo 100 caracteres");

            // Financeiro
            RuleFor(x => x.ValorSessao)
                .GreaterThan(0).WithMessage("Valor da sessão deve ser maior que zero")
                .LessThanOrEqualTo(9999.99m).WithMessage("Valor da sessão deve ser menor ou igual a R$ 9.999,99");

            RuleFor(x => x.FormaPagamentoPreferida)
                .NotEmpty().WithMessage("Forma de pagamento preferida é obrigatória")
                .Must(ValidarFormaPagamento).WithMessage("Forma de pagamento inválida");

            RuleFor(x => x.DiaVencimento)
                .InclusiveBetween(1, 31).WithMessage("Dia de vencimento deve estar entre 1 e 31");

            // Contato de Emergência
            RuleFor(x => x.ContatoEmergencia)
                .MaximumLength(100).WithMessage("Contato de emergência deve ter no máximo 100 caracteres");

            RuleFor(x => x.TelefoneEmergencia)
                .Matches(@"^\(\d{2}\)\s\d{4,5}-\d{4}$")
                .WithMessage("Telefone de emergência deve estar no formato (XX) XXXXX-XXXX")
                .When(x => !string.IsNullOrEmpty(x.TelefoneEmergencia));

            // Profissão
            RuleFor(x => x.Profissao)
                .MaximumLength(50).WithMessage("Profissão deve ter no máximo 50 caracteres");

            // LGPD
            RuleFor(x => x.AceiteLgpd)
                .Equal(true).WithMessage("É obrigatório aceitar os termos da LGPD");

            // Observações
            RuleFor(x => x.Observacoes)
                .MaximumLength(500).WithMessage("Observações devem ter no máximo 500 caracteres");
        }

        private static bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            // Remove formatação
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.All(c => c == cpf[0]))
                return false;

            // Validação do primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cpf[9].ToString()) != digito1)
                return false;

            // Validação do segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return int.Parse(cpf[10].ToString()) == digito2;
        }

        private static bool ValidarFormaPagamento(string formaPagamento)
        {
            var formasValidas = new[] { "DINHEIRO", "PIX", "CARTAO_DEBITO", "CARTAO_CREDITO", "TRANSFERENCIA" };
            return formasValidas.Contains(formaPagamento.ToUpper());
        }
    }
}
