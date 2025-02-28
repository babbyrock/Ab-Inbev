using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);
        Task<Rating> CreateRatingAsync(Rating rating, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Product> GetByIdAsync(int productId, CancellationToken cancellationToken = default); 
        Task DeleteAsync(Product product, CancellationToken cancellationToken = default); 
        Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
    }
}
