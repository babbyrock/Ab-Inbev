using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<SaleResult>
    {
        public string Customer { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public int CartId { get; set; }
    }

}
