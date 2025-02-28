using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCartCommand, Cart>()
           .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));  // Mapeamento explícito para a lista de produtos
            CreateMap<UpdateCartCommand, Cart>();
            CreateMap<Cart, CartResult>();
            CreateMap<CartResult, Cart>();
            CreateMap<CartCartItem, CartItem>();
            CreateMap<CartItem, CartCartItem>();
        }
    }
}
