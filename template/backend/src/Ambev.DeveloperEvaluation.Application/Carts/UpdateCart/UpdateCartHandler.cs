using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, CartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public UpdateCartHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartResult> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCartValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.ToString());

            var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);
            if (cart == null)
                throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

            var itemsToRemove = new List<CartItem>();

            foreach (var item in cart.Products)
            {
                if (!request.Products.Any(p => p.Id == item.Id))
                {
                    itemsToRemove.Add(item);
                }
            }
            request.Products.RemoveAll(p => cart.Products.Any(c => c.ProductId == p.ProductId && p.Id == 0));

            await _cartRepository.DeleteCartItemsAsync(itemsToRemove, cancellationToken);

            cart.Date = DateTime.UtcNow; // Atualiza a data
            cart.Products = request.Products.Select(item => new CartItem
            {
                CartId = cart.Id, // Usar o ID do carrinho existente
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            }).ToList();

            await _cartRepository.UpdateAsync(cart, itemsToRemove,  cancellationToken);

            await _cartRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CartResult>(cart);
        }
    }
}
