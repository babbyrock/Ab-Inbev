using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class SaleDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public string Branch { get; set; }
        public bool IsCanceled { get; set; }
        public List<SaleItemDTO> Items { get; set; } = new List<SaleItemDTO>();
    }
}
