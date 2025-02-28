using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;

        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task<Rating> CreateRatingAsync(Rating rating, CancellationToken cancellationToken = default)
        {
            await _context.Ratings.AddAsync(rating, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return rating;
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Products.Include(p => p.Rating).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Product> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Include(p => p.Rating).AsNoTracking()
                                 .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
        }

        public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
