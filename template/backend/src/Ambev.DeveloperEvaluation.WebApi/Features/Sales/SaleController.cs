using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        //private readonly ISaleService _saleService;

        public SaleController()
        {
            //_saleService = saleService;
        }

        [HttpGet("create-sale/{cartId}")]
        public async Task<IActionResult> CreateSaleFromCart(int cartId)
        {
            try
            {
                //var saleDTO = await _saleService.CreateSaleAsync(cartId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
