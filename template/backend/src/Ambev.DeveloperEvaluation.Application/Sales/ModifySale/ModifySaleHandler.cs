using Ambev.DeveloperEvaluation.Application.Sales.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Application.Sales.ModifySale
{
    public class ModifySaleCommandHandler : IHandleMessages<SaleModifiedEvent>
    {
        private readonly IBus _bus;
        private readonly ILogger<ModifySaleCommandHandler> _logger;
        private readonly ISaleRepository _saleRepository;

        public ModifySaleCommandHandler(IBus bus, ILogger<ModifySaleCommandHandler> logger, ISaleRepository saleRepository)
        {
            _bus = bus;
            _logger = logger;
            _saleRepository = saleRepository;
        }

        public async Task Handle(SaleModifiedEvent request)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
            {
                _logger.LogError($"Sale with ID {request.SaleId} not found.");
                throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found.");
            }

            sale.Customer = request.Customer;
            sale.Branch = request.Branch;

            await _saleRepository.UpdateAsync(sale);
            await _saleRepository.SaveChangesAsync();

            await Task.CompletedTask;

        }
    }

}
