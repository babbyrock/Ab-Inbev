using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using FluentValidation;
using System;

namespace Ambev.DeveloperEvaluation.Application.Carts
{
    public class CreateCartValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required");

            RuleFor(x => x.Products)
                .NotEmpty()
                .WithMessage("The 'Products' field cannot be empty.");

            RuleForEach(x => x.Products)
                .NotNull()
                .WithMessage("Each product must be valid.");

            RuleForEach(x => x.Products)
                .Must(product => product.ProductId > 0)
                .WithMessage("Each product must have an Id greater than zero.");
        }
    }
}
