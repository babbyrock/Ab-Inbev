using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleCreatedEvent, SaleResult>();

            CreateMap<CartItem, SaleItemResult>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 


            CreateMap<SaleResult, Sale>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<Sale, SaleResult>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<CreateSaleCommand, SaleResult>();



            CreateMap<SaleItemResult, SaleItem>().ForMember(dest => dest.Sale, opt => opt.Ignore());
            CreateMap<SaleItem, SaleItemResult>();
        }
    }
}
