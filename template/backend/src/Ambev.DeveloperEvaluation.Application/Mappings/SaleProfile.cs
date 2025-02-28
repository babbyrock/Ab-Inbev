using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Mappings
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Cart, SaleDTO>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.UserId.ToString()))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.TotalSaleAmount, opt => opt.MapFrom(src => src.Products.Sum(p => p.Quantity * p.Product.Price)));

            CreateMap<CartItem, SaleItemDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price));  
        }
    }
}
