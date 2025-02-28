using Ambev.DeveloperEvaluation.Application.Sales.Events;
using Ambev.DeveloperEvaluation.Application.Sales.ModifySale;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.SaleCancelledItem
{
    public class SaleCancelledItemEventHandler : IHandleMessages<SaleCancelledItemEvent>
    {
        private readonly IBus _bus;
        private readonly ILogger<SaleCancelledItemEventHandler> _logger;
        private readonly ISaleRepository _saleRepository;

        public SaleCancelledItemEventHandler(IBus bus, ILogger<SaleCancelledItemEventHandler> logger, ISaleRepository saleRepository)
        {
            _bus = bus;
            _logger = logger;
            _saleRepository = saleRepository;
        }

        public async Task Handle(SaleCancelledItemEvent request)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
            {
                _logger.LogError($"Sale with ID {request.SaleId} not found.");
                throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found.");
            }

            foreach (var item in sale.Items)
            {
                if(item.Id == request.ItemId)
                {
                    if (item == null)
                    {
                        _logger.LogError($"Sale with ID {request.ItemId} not found.");
                        throw new KeyNotFoundException($"Sale with ID {request.ItemId} not found.");
                    }
                    item.IsCanceled = true;
                }
            }


            await _saleRepository.UpdateAsync(sale);
            await _saleRepository.SaveChangesAsync();

            await Task.CompletedTask;

        }
    }
}
