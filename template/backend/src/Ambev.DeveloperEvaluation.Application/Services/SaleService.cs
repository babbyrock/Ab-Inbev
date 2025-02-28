using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;
using AutoMapper;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;  // Repositório do Cart, para pegar o Cart com seus Itens

        public SaleService(IMapper mapper, ICartRepository cartRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<SaleDTO> CreateSaleAsync(int cartId)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            if (cart == null)
                throw new Exception("Cart not found");

            var saleDTO = _mapper.Map<SaleDTO>(cart);

            return saleDTO;
        }
    }
}
