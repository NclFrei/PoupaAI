using Financias.API.Domain.DTOs.Request;
using FluentValidation;

namespace Financias.API.Domain.Validator;

public class CategoriaCreateRequestValidator : AbstractValidator<CategoriaRequest>
{
    public CategoriaCreateRequestValidator()
    {
        RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(c => c.Icone)
            .NotEmpty().WithMessage("O ícone é obrigatório.")
            .MaximumLength(50).WithMessage("O ícone deve ter no máximo 50 caracteres.");

        RuleFor(c => c.CorHex)
            .NotEmpty().WithMessage("A cor é obrigatória.");
    }
}