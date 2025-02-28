namespace Ambev.DeveloperEvaluation.Application.Products
{
    /// <summary>
    /// Represents the rating details of a product.
    /// </summary>
    public class ProductRating
    {
        /// <summary>
        /// Gets or sets the average rating value.
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the total number of ratings.
        /// </summary>
        public int? Count { get; set; }
    }
}
