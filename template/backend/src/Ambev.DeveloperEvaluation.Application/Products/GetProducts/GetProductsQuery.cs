using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    public class GetProductsQuery : QueryStringParameters, IRequest<PagedList<ProductResult>>
    {
        /// <summary>
        /// Parâmetros de consulta para paginação e ordenação dos produtos
        /// </summary>
        public string? Title { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int? Count { get; set; }
        public int? Rate { get; set; }
    }
}
