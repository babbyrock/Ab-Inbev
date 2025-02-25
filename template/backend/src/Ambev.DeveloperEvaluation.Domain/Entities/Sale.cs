using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale in the system with authentication and profile information.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// The unique identifier of the sale
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The date of the sale
        /// </summary>
        public DateTime Date { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The customer who purchased
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// The total amount of the sale, calculated based on the products.
        /// </summary>
        public decimal TotalSaleAmount { get; private set; }

        /// <summary>
        /// The branch where the sale was made.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the sale has been canceled.
        /// </summary>
        public bool IsCanceled { get; set; }

        /// <summary>
        /// Gets the list of items associated with this sale.
        /// </summary>
        public List<SaleItem> Items { get; set; } = new List<SaleItem>();

        /// <summary>
        /// Calculates the total sale amount based on the product sales.
        /// </summary>
        public void CalculateTotalSaleAmount()
        {
            TotalSaleAmount = Items.Sum(item => item.TotalAmount);
        }
    }
}
