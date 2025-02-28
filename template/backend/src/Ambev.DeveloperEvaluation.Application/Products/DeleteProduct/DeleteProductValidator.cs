using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        /// <summary>
        /// Initializes validation rules for DeleteProductCommand
        /// </summary>
        public DeleteProductValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product ID is required");
        }
    }
}
