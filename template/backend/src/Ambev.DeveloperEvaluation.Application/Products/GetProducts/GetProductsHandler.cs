using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, PagedList<ProductResult>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<ProductResult>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {

            var validator = new GetProductsValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.ToString());


            var products = await _productRepository.GetAllAsync();


            if (!string.IsNullOrWhiteSpace(request.Title))
                products = products.Where(p => p.Title.ToLower().Contains(request.Title)).ToList();

            if (!string.IsNullOrWhiteSpace(request.Category))
                products = products.Where(p => p.Category.ToLower().Contains(request.Category)).ToList();

            if (!string.IsNullOrWhiteSpace(request.Description))
                products = products.Where(p => p.Description.ToLower().Contains(request.Description)).ToList();

            if (request.MinPrice.HasValue)
                products = products.Where(p => p.Price >= request.MinPrice.Value).ToList();

            if (request.MaxPrice.HasValue)
                products = products.Where(p => p.Price <= request.MaxPrice.Value).ToList();

            if (request.Count.HasValue)
                products = products.Where(p => p.Rating is not null && p.Rating.Count >= request.Count.Value).ToList();

            if (request.Rate.HasValue)
                products = products.Where(p => p.Rating is not null && p.Rating.Rate <= request.Rate.Value).ToList();

            var productsQuery = products.AsQueryable();

            productsQuery = OrderingHelper.ApplyOrdering(productsQuery, request._order);

            var mappedProducts = _mapper.ProjectTo<ProductResult>(productsQuery);

            var pagedProducts = PagedList<ProductResult>.ToPagedList(
                mappedProducts,
                request.PageNumber,
                request.PageSize
            );

            return pagedProducts;
        }
    }
}
