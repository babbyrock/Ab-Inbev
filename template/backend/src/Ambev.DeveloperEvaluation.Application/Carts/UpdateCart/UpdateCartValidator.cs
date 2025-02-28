using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartValidator()
        {

            RuleFor(x => x.Products)
                .Must(products => products == null || products.Count > 0)
                .WithMessage("If provided, the Products list must contain at least one product.");
        }
    }
}
