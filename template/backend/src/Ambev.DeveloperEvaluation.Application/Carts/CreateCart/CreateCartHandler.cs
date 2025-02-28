using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CreateCartHandler(IMapper mapper, ICartRepository cartRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<CartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateCartValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);


            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.ToString());

            var cart = _mapper.Map<Cart>(command);
            cart.Date = DateTime.UtcNow;

            var createdCart = await _cartRepository.CreateAsync(cart, cancellationToken);
            await _cartRepository.SaveChangesAsync(cancellationToken);
            var result = _mapper.Map<CartResult>(createdCart);

            return result;
        }
    }
}
