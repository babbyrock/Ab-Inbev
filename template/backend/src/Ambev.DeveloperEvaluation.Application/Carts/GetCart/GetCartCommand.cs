using Ambev.DeveloperEvaluation.Application.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartCommand : IRequest<CartResult>
    {
        public int Id { get; set; }

        public GetCartCommand(int id)
        {
            Id = id;
        }
    }
}
