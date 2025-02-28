using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCarts
{
    public class GetCartsCommand : QueryStringParameters, IRequest<PagedList<CartResult>>
    {
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}
