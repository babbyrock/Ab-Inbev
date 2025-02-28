using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCarts
{
    public class GetCartsHandler : IRequestHandler<GetCartsCommand, PagedList<CartResult>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public GetCartsHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<CartResult>> Handle(GetCartsCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetCartsValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.ToString());
            }

            var carts = await _cartRepository.GetAllAsync();

            if (request.MinDate.HasValue)
                carts = carts.Where(c => c.Date >= request.MinDate.Value).ToList();

            if (request.MaxDate.HasValue)
                carts = carts.Where(c => c.Date <= request.MaxDate.Value).ToList();

            var cartsQuery = carts.AsQueryable();
            cartsQuery = OrderingHelper.ApplyOrdering(cartsQuery, request._order);

            var mappedCarts = _mapper.ProjectTo<CartResult>(cartsQuery);

            var pagedCarts = PagedList<CartResult>.ToPagedList(
                mappedCarts,
                request.PageNumber,
                request.PageSize
            );

            return pagedCarts;
        }
    }
}
