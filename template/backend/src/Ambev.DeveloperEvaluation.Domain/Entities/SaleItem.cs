namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale item in the system with authentication and profile information.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class SaleItem
    {
        // <summary>
        /// The unique identifier of the sale item
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the unique identifier of the sale to which this item belongs.
        /// </summary>
        public Guid SaleId { get; set; }

        // <summary>
        /// The sale to which this item belongs.
        /// </summary>
        public Sale Sale { get; set; } // Propriedade de navegação adicionada

        /// <summary>
        /// Gets the unique identifier of the product associated with this sale item.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets the product associated with this sale item.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets the quantity of the product purchased.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the unit price of the product at the time of sale.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets the discount applied to this specific sale item.
        /// </summary>
        public decimal Discount { get; set; } = 0.0m;

        /// <summary>
        /// Gets the total amount for the item, calculated as (Quantity * UnitPrice) - Discount.
        /// </summary>
        public decimal TotalAmount
        {
            get
            {
                return (Quantity * UnitPrice) - Discount;
            }
            set { }
        }

        /// <summary>
        /// Indicates if the sale item has been canceled.
        /// </summary>
        public bool IsCanceled { get; set; } = false; // Campo IsCanceled, default é false (não cancelado)

        // Constructor to initialize SaleItem with an Id
        public SaleItem()
        {
            Id = Guid.NewGuid(); // Generates a new unique identifier
        }
    }
}
