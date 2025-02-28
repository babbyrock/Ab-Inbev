using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> AddAsync(Sale sale, CancellationToken cancellationToken);
        Task<Sale> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
