using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Sale> AddAsync(Sale sale, CancellationToken cancellationToken)
        {
             await _context.Sales.AddAsync(sale, cancellationToken);
            return sale;
        }

        public async Task<Sale> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Sales
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Sales.ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
