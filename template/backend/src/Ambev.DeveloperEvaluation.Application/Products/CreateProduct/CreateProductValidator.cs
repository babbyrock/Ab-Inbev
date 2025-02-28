using FluentValidation;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for product creation.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with validation rules.
    /// </summary>
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Title).NotEmpty().Length(2, 100).WithMessage("Title is required (2-100 characters).");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(p => p.Category).NotEmpty().Length(3, 50).WithMessage("Category is required (3-50 characters).");
        RuleFor(p => p.Image).NotEmpty().Matches(@"^.*\.(jpg|jpeg|png|gif|bmp)$", RegexOptions.IgnoreCase)
            .WithMessage("Invalid image format. Allowed formats: jpg, jpeg, png, gif, bmp.");
        RuleFor(p => p.Rating.Rate).InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");
    }
}
