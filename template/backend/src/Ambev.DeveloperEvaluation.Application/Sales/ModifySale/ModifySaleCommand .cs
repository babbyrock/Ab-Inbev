using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.ModifySale
{
    public class ModifySaleCommand : IRequest
    {
        public Guid SaleId { get; }
        public string Customer { get; }
        public string Branch { get; }

        public ModifySaleCommand(Guid saleId, string customer, string branch)
        {
            SaleId = saleId;
            Customer = customer;
            Branch = branch;
        }
    }

}
