using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Bus;

public class SaleService
{
    private readonly IBus _bus;

    public SaleService(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishSaleCreatedEvent(Guid saleId, string customerName)
    {
        // Criação da instância de Sale
        var sale = new Sale
        {
            Id = saleId,
            Customer = customerName,
            Date = DateTime.UtcNow
        };
        
    }
}
