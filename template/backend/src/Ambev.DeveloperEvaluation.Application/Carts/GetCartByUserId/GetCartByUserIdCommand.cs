using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByUserId
{
    public class GetCartByUserIdCommand : IRequest<CartResult>
    {
        public Guid UserId { get; set; }

        public GetCartByUserIdCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
