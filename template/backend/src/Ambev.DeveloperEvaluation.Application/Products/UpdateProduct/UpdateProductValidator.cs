using FluentValidation;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(2, 100).WithMessage("Title must be between 2 and 100 characters.")
                .When(p => p.Title is not null);

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.")
                .When(p => p.Price > 0);

            RuleFor(p => p.Category)
                .NotEmpty().WithMessage("Category is required.")
                .Length(3, 50).WithMessage("Category must be between 3 and 50 characters.")
                .When(p => p.Category is not null);

            RuleFor(p => p.Rating.Rate)
                .InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.")
                .When(p => p.Rating is not null);
        }
    }
}
