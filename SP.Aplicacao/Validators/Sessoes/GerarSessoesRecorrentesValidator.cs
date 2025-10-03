using FluentValidation;
using SP.Aplicacao.DTOs.Sessoes;
using SP.Dominio.Enums;

namespace SP.Aplicacao.Validators.Sessoes;

/// <summary>
/// Validador para geração de sessões recorrentes
/// </summary>
public class GerarSessoesRecorrentesValidator : AbstractValidator<GerarSessoesRecorrentesDto>
{
    public GerarSessoesRecorrentesValidator()
    {
        RuleFor(x => x.ClienteId)
            .GreaterThan(0)
            .WithMessage("Cliente é obrigatório");

        RuleFor(x => x.DataInicio)
            .NotEmpty()
            .WithMessage("Data de início é obrigatória")
            .GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("Data de início deve ser hoje ou futura");

        RuleFor(x => x.DataFim)
            .NotEmpty()
            .WithMessage("Data de fim é obrigatória")
            .GreaterThan(x => x.DataInicio)
            .WithMessage("Data de fim deve ser posterior à data de início");

        RuleFor(x => x.DataFim)
            .LessThanOrEqualTo(x => x.DataInicio.AddYears(1))
            .WithMessage("Período não pode exceder 1 ano");

        RuleFor(x => x.Periodicidade)
            .IsInEnum()
            .WithMessage("Periodicidade inválida")
            .NotEqual(PeriodicidadeSessao.Livre)
            .WithMessage("Periodicidade 'Livre' não é válida para geração automática");

        RuleFor(x => x.HorarioSessao)
            .Must(BeValidTime)
            .WithMessage("Horário deve estar entre 06:00 e 22:00");

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("Valor deve ser maior que zero")
            .LessThanOrEqualTo(9999.99m)
            .WithMessage("Valor não pode exceder R$ 9.999,99");

        RuleFor(x => x.DuracaoMinutos)
            .GreaterThan(0)
            .WithMessage("Duração deve ser maior que zero")
            .LessThanOrEqualTo(480)
            .WithMessage("Duração não pode exceder 8 horas");

        RuleFor(x => x.Observacoes)
            .MaximumLength(1000)
            .WithMessage("Observações não podem exceder 1000 caracteres");
    }

    private static bool BeValidTime(TimeSpan horario)
    {
        return horario >= TimeSpan.FromHours(6) && horario <= TimeSpan.FromHours(22);
    }
}
