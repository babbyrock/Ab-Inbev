using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default);
        Task<List<Cart>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Cart> GetByIdAsync(int cartId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Cart cart, List<CartItem> itemsToRemove, CancellationToken cancellationToken = default);
        Task DeleteCartItemsAsync(List<CartItem> itemsToRemove, CancellationToken cancellationToken = default);
        Task DeleteAsync(Cart cart, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<Cart> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
