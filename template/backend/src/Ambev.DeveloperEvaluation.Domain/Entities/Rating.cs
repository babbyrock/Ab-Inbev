namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a rating in the system with authentication and profile information.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// The unique identifier of the rating.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The identifier of the product being rated.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The rating value given to the product (1 to 5).
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// The number of times this rating was given.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// The date when the rating was made.
        /// </summary>
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
