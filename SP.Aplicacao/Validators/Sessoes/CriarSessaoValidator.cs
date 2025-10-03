using FluentValidation;
using SP.Aplicacao.DTOs.Sessoes;
using SP.Dominio.Enums;

namespace SP.Aplicacao.Validators.Sessoes;

/// <summary>
/// Validador para criação de sessão
/// </summary>
public class CriarSessaoValidator : AbstractValidator<CriarSessaoDto>
{
    public CriarSessaoValidator()
    {
        RuleFor(x => x.ClienteId)
            .GreaterThan(0)
            .WithMessage("Cliente é obrigatório");

        RuleFor(x => x.DataHoraAgendada)
            .NotEmpty()
            .WithMessage("Data e hora são obrigatórias")
            .GreaterThan(DateTime.Now)
            .WithMessage("Data e hora devem ser futuras");

        RuleFor(x => x.DuracaoMinutos)
            .GreaterThan(0)
            .WithMessage("Duração deve ser maior que zero")
            .LessThanOrEqualTo(480)
            .WithMessage("Duração não pode exceder 8 horas");

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("Valor deve ser maior que zero")
            .LessThanOrEqualTo(9999.99m)
            .WithMessage("Valor não pode exceder R$ 9.999,99");

        RuleFor(x => x.Periodicidade)
            .IsInEnum()
            .WithMessage("Periodicidade inválida");

        RuleFor(x => x.Observacoes)
            .MaximumLength(1000)
            .WithMessage("Observações não podem exceder 1000 caracteres");

        // Validação de horário comercial (opcional)
        RuleFor(x => x.DataHoraAgendada)
            .Must(BeInBusinessHours)
            .WithMessage("Sessões devem ser agendadas entre 6h e 22h")
            .When(x => x.DataHoraAgendada != default);
    }

    private static bool BeInBusinessHours(DateTime dataHora)
    {
        var hora = dataHora.Hour;
        return hora >= 6 && hora <= 22;
    }
}
