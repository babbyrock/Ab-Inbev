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

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelledSale
{
    public class SaleCancelledEventHandler : IHandleMessages<SaleCancelledEvent>
    {
        private readonly ILogger<SaleCancelledEventHandler> _logger;
        private readonly ISaleRepository _saleRepository;
        private readonly IBus _bus;

        public SaleCancelledEventHandler(ILogger<SaleCancelledEventHandler> logger, ISaleRepository saleRepository, IBus bus)
        {
            _logger = logger;
            _saleRepository = saleRepository;
            _bus = bus;
        }

        public async Task Handle(SaleCancelledEvent message)
        {
            _logger.LogInformation($"Handling SaleCancelledEvent for SaleId: {message.SaleId}");

            var sale = await _saleRepository.GetByIdAsync(message.SaleId);
            if (sale == null)
            {
                _logger.LogError($"Venda não encontrada. SaleId: {message.SaleId}");
                throw new Exception($"Venda não encontrada. SaleId: {message.SaleId}");
            }

            sale.IsCanceled = true; 

            await _saleRepository.UpdateAsync(sale);
            await _saleRepository.SaveChangesAsync();

            _logger.LogInformation($"Venda {message.SaleId} foi cancelada com sucesso.");
        }
    }
}
