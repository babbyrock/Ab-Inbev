using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class CreateCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly CreateCartHandler _handler;

        public CreateCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateCartHandler(_mapper, _cartRepository);
        }

        [Fact(DisplayName = "Given valid cart data When creating cart Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var command = new CreateCartCommand
            {
                UserId = 1,
                Products = new List<CartCartItem>
            {
                new CartCartItem { Id = 1, ProductId = 1, Quantity = 2 },
                new CartCartItem { Id = 2, ProductId = 2, Quantity = 1 }
            }
            };

            var cart = new Cart
            {
                Id = 1,
                UserId = command.UserId,
                Date = DateTime.UtcNow,
                Products = command.Products.Select(product => new CartItem
                {
                    CartId = 1,
                    Id = product.Id,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                }).ToList()
            };

            var cartResult = new CartResult
            {
                Id = 1, // Ajuste: Defina um ID válido ou use um mock do repositório
                UserId = cart.UserId,
                Date = cart.Date,
                Products = cart.Products.Select(item => new CartCartItem
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList()
            };

            // Mock do Mapper
            _mapper.Map<Cart>(command).Returns(cart);
            _mapper.Map<CartResult>(cart).Returns(cartResult);// Aqui deve ser mockado corretamente

            _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(cart));  // Retorna o cart simulado


            // Act
            var createCartResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            createCartResult.Should().NotBeNull();
            createCartResult.Id.Should().Be(cartResult.Id); // ID igual ao esperado
            createCartResult.UserId.Should().Be(cartResult.UserId); // Verifica o ID do usuário
            createCartResult.Products.Should().HaveCount(cartResult.Products.Count); // Verifica a quantidade de produtos
            await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        }


        [Fact(DisplayName = "Given invalid cart data When creating cart Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            var command = new CreateCartCommand(); // Invalid command with missing fields

            var act = () => _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given valid command When handling Then maps command to cart entity")]
        public async Task Handle_ValidRequest_MapsCommandToCart()
        {
            var command = new CreateCartCommand
            {
                UserId = 1,
                Products = new List<CartCartItem>
            {
                new CartCartItem { Id = 1, ProductId = 1, Quantity = 2 },
                new CartCartItem { Id = 2, ProductId = 2, Quantity = 1 }
            }
            };

            var cart = new Cart
            {
                Id = 1,
                UserId = command.UserId,
                Date = DateTime.UtcNow,
                Products = command.Products.Select(product => new CartItem
                {
                    CartId = 1,
                    Id = product.Id,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                }).ToList()
            };

            _mapper.Map<Cart>(command).Returns(cart);
            _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
                .Returns(cart);

            await _handler.Handle(command, CancellationToken.None);

            _mapper.Received(1).Map<Cart>(Arg.Is<CreateCartCommand>(c =>
                c.UserId == command.UserId &&
                c.Products.Count == command.Products.Count));
        }
    }
}
