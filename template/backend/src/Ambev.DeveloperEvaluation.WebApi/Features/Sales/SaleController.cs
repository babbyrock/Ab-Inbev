using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Bus;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Application.Sales.ModifySale;
using Ambev.DeveloperEvaluation.Application.Sales.Events;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly SaleService _saleService;
        private readonly ILogger<SaleController> _logger;
        private readonly IBus _bus;

        public SaleController(IMediator mediator, IMapper mapper, SaleService saleService, ILogger<SaleController> logger, IBus bus)
        {
            _mediator = mediator;
            _mapper = mapper;
            _saleService = saleService;
            _logger = logger;
            _bus = bus;
        }

        [HttpPost("create-sale")]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
        {

            var saleCreatedEvent = new SaleCreatedEvent
            {
                Customer = command.Customer,
                Branch = command.Branch,
                CartId = command.CartId
            };
            await _bus.Send(saleCreatedEvent);

            return Ok(new { message = "Sale created successfully" });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetSaleCommand(id);
            var result = await _mediator.Send(query);

            return Ok(new ApiResponseWithData<SaleResult>
            {
                Success = true,
                Message = "Sale retrieved successfully",
                Data = _mapper.Map<SaleResult>(result)
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleRequest request)
        {
            switch (request.Action)
            {
                case ActionType.Modify:
                    if (request.Customer == null || request.Branch == null)
                        return BadRequest("Customer and Branch are required for modification.");

                    await _bus.Send(new SaleModifiedEvent(id, request.Customer, request.Branch));
                    break;

                case ActionType.CancelSale:
                    await _bus.Send(new SaleCancelledEvent(id));
                    break;

                case ActionType.CancelItem:
                    if (request.ItemId == null)
                        return BadRequest("ItemId is required for canceling an item.");

                    await _bus.Send(new SaleCancelledItemEvent(id, request.ItemId.Value));
                    break;

                default:
                    return BadRequest("Invalid action.");
            }

            return NoContent();
        }
    }

    public record UpdateSaleRequest(ActionType Action, Guid? ItemId, string? Customer, string? Branch);
}
