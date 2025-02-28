using Rebus.Handlers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class SaleCreatedEventHandler : IHandleMessages<SaleCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IBus _bus;
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleCreatedEventHandler> _logger;

        public SaleCreatedEventHandler(ICartRepository cartRepository, IBus bus, ISaleRepository saleRepository, ILogger<SaleCreatedEventHandler> logger, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _bus = bus;
            _saleRepository = saleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Handle(SaleCreatedEvent message)
        {
            _logger.LogInformation("Iniciando a criação da venda...");

            var cart = await _cartRepository.GetByIdAsync(message.CartId);
            if (cart == null)
            {
                _logger.LogError("Carts not found.");
                throw new Exception("Carts not found");
            }
            var saleResult = _mapper.Map<SaleResult>(message);
            var saleItemsResult = _mapper.Map<List<SaleItemResult>>(cart.Products);
            saleResult.Items = saleItemsResult;

            foreach (var item in saleResult.Items)
            {
                if (item.Quantity > 20)
                {
                    _logger.LogWarning($"Cannot sell more than 20 units of Product {item.ProductId}. Quantity: {item.Quantity}");
                    throw new Exception($"Cannot sell more than 20 units of Product {item.ProductId}");
                }

                if (item.Quantity >= 4 && item.Quantity < 10) item.Discount = 0.10m;
                if (item.Quantity >= 10 && item.Quantity <= 20) item.Discount = 0.20m;
            }

            saleResult.CalculateTotalSaleAmount();

            

            var saleMapped = _mapper.Map<Sale>(saleResult);

            _logger.LogInformation("Salvando a venda no repositório...");

            await _saleRepository.AddAsync(saleMapped);
            await _saleRepository.SaveChangesAsync();

            _logger.LogInformation("Venda salva com sucesso. Publicando evento de criação da venda...");

            _logger.LogInformation($"Venda criada com sucesso. SaleId: {saleMapped.Id}");

            await Task.CompletedTask;
        }
    }
}
