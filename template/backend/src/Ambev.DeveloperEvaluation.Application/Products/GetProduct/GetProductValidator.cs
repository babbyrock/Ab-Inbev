using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductValidator : AbstractValidator<GetProductCommand>
    {
        public GetProductValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product ID is required");
        }
    }
}
