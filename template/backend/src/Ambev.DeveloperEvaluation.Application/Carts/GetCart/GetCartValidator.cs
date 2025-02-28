using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartValidator : AbstractValidator<GetCartCommand>
    {
        public GetCartValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Cart ID is required");
        }
    }
}
