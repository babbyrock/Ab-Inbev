using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Carts.GetCarts;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCartByUserId;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateCartCommand>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCart([FromBody] CreateCartCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCartValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _mediator.Send(request, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CartResult>
            {
                Success = true,
                Message = "Cart created successfully",
                Data = _mapper.Map<CartResult>(response)
            });
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCarts([FromQuery] GetCartsCommand query, CancellationToken cancellationToken)
        {
            var validator = new GetCartsValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = await _mediator.Send(query);

            return Ok(new ApiResponseWithData<PagedList<CartResult>>
            {
                Success = true,
                Message = "Carts retrieved successfully",
                Data = result
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<CartResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCart([FromRoute] int id, CancellationToken cancellationToken)
        {
            var request = new GetCartCommand(id);
            var validator = new GetCartValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetCartCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<CartResult>
            {
                Success = true,
                Message = "Cart retrieved successfully",
                Data = _mapper.Map<CartResult>(response)
            });
        }

        [HttpGet("user/{userId}")] // Adiciona um prefixo "user" para diferenciar
        [ProducesResponseType(typeof(ApiResponseWithData<CartResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCartByUserId([FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            var request = new GetCartByUserIdCommand(userId);

            // Envia o comando para obter o carrinho
            var response = await _mediator.Send(request, cancellationToken);

            // Verifica se a resposta é nula ou se o carrinho não foi encontrado
            if (response == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Carrinho não encontrado."
                });
            }

            // Se o carrinho foi encontrado, retorna a resposta
            return Ok(new ApiResponseWithData<CartResult>
            {
                Success = true,
                Message = "Carrinho recuperado com sucesso.",
                Data = _mapper.Map<CartResult>(response)
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<CartResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCart([FromRoute] int id, [FromBody] UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCartValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            request.Id = id;

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponseWithData<CartResult>
            {
                Success = true,
                Message = "Cart updated successfully",
                Data = _mapper.Map<CartResult>(response)
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCart(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteCartCommand(id);
            var validator = new DeleteCartValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new DeleteCartCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (result)
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Cart deleted successfully"
                });

            return NotFound();
        }
    }
}
