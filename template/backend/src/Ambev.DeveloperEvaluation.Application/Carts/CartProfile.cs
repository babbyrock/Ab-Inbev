using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CreateCartCommand, Cart>();
            CreateMap<UpdateCartCommand, Cart>();
            CreateMap<Cart, CartResult>();
            CreateMap<CartResult, Cart>();
            CreateMap<CartCartItem, CartItem>();
            CreateMap<CartItem, CartCartItem>();
        }
    }
}
