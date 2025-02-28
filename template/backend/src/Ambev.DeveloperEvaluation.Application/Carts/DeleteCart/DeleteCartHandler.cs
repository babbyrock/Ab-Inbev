using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, bool>
    {
        private readonly ICartRepository _cartRepository;

        public DeleteCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {

            var validator = new DeleteCartValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.ToString());


            var cart = await _cartRepository.GetByIdAsync(request.Id);
            if (cart is null)
            {
                if (cart == null)
                    throw new KeyNotFoundException($"Cart with ID {request.Id} not found");
            }

            await _cartRepository.DeleteAsync(cart);

            await _cartRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
