﻿using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
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
            CreateMap<UpdateCartCommand, Cart>()
                 .ForMember(dest => dest.Products, opt => opt.MapFrom(src =>
                     src.Products.Select(item =>
                         new CartItem
                         {
                             CartId = src.Id,
                             Id = item.Id,
                             ProductId = item.ProductId,
                             Quantity = item.Quantity
                         })));
            CreateMap<Cart, CartResult>().ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products)); ;
            CreateMap<CartResult, Cart>();
            CreateMap<CartCartItem, CartItem>();
            CreateMap<CartItem, CartCartItem>();
        }
    }
}
