using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;

        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
            return cart;
        }

        public async Task<List<Cart>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Carts.Include(c => c.Products)
                .ThenInclude(c => c.Product).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Cart> GetByIdAsync(int cartId, CancellationToken cancellationToken = default)
        {
            return await _context.Carts.Include(c => c.Products).ThenInclude(c => c.Product)
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);
        }

        public Task UpdateAsync(Cart cart, List<CartItem> itemsToRemove, CancellationToken cancellationToken = default)
        {
            _context.Carts.Update(cart);
            return Task.CompletedTask;
        }

        public Task DeleteCartItemsAsync(List<CartItem> itemsToRemove, CancellationToken cancellationToken = default)
        {
            _context.CartItems.RemoveRange(itemsToRemove);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            _context.Carts.Remove(cart);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
