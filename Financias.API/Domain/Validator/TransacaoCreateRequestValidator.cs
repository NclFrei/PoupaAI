using Financias.API.Domain.DTOs.Request;
using FluentValidation;

namespace Financias.API.Domain.Validator;

public class TransacaoCreateRequestValidator : AbstractValidator<TransacaoRequest>
{
    public TransacaoCreateRequestValidator()
    {
        RuleFor(t => t.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(t => t.Descricao)
            .MaximumLength(255).WithMessage("A descrição deve ter no máximo 255 caracteres.");

        RuleFor(t => t.DataTransacao)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("A data da transação não pode ser no futuro.");

        RuleFor(t => t.Valor)
            .GreaterThan(0).WithMessage("O valor deve ser maior que zero.");

        RuleFor(t => t.CategoriaId)
            .GreaterThan(0).WithMessage("A categoria é obrigatória.");

        RuleFor(t => t.UsuarioId)
            .GreaterThan(0).WithMessage("O usuário é obrigatório.");

        RuleFor(t => t.Tipo)
            .IsInEnum().WithMessage("O tipo de transação é inválido.");
    }
}