using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class SaleResult
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Customer { get; set; }
        public decimal TotalSaleAmount { get; private set; }
        public string Branch { get; set; }
        public bool IsCanceled { get; set; }
        public List<SaleItemResult> Items { get; set; } = new List<SaleItemResult>();

        public void CalculateTotalSaleAmount()
        {
            TotalSaleAmount = Items.Sum(item => item.TotalAmount);
        }
    }
}
