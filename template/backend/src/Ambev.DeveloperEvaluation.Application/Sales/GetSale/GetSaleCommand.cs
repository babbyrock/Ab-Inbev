using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleCommand : IRequest<SaleResult> 
    {
        public Guid SaleId { get; set; }

        public GetSaleCommand(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
