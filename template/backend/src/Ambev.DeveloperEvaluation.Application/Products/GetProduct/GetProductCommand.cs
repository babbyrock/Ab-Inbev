using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductCommand : IRequest<ProductResult> 
    {
        public int ProductId { get; set; }

        public GetProductCommand(int productId)
        {
            ProductId = productId;
        }
    }
}
