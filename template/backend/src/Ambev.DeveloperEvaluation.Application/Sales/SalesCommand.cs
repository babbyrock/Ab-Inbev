using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public record CancelSaleCommand(Guid SaleId) : IRequest;
    public record CancelItemCommand(Guid SaleId, Guid ItemId) : IRequest;
}
