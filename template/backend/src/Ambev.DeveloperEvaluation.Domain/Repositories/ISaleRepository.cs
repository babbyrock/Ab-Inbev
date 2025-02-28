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
        Task<Sale> AddAsync(Sale sale);
        Task<Sale> GetByIdAsync(Guid id);
        Task<IEnumerable<Sale>> GetAllAsync();
        Task UpdateAsync(Sale sale);
        Task SaveChangesAsync();
    }

}
