using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    public class GetProductsValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(100)
                .WithMessage("Title must be at most 100 characters long.");


            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MaxPrice must be greater than or equal to 0.")
                .GreaterThanOrEqualTo(x => x.MinPrice ?? 0)
                .WithMessage("MaxPrice must be greater than or equal to MinPrice.");

            RuleFor(x => x.Category)
                .MaximumLength(50)
                .WithMessage("Category must be at most 50 characters long.");

            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Description must be at most 255 characters long.");

            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Count must be greater than or equal to 0.");

            RuleFor(x => x.Rate)
                .InclusiveBetween(0, 5)
                .WithMessage("Rate must be between 0 and 5.");
        }
    }
}
