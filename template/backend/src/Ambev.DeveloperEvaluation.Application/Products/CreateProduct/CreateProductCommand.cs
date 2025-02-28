using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    /// <summary>
    /// Command for creating a new product.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for creating a product,  
    /// including title, price, description, category, image URL, and rating.  
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request  
    /// that returns a <see cref="ProductResult"/>.  
    ///  
    /// The data provided in this command is validated using the  
    /// <see cref="CreateProductCommandValidator"/> which extends  
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly  
    /// populated and follow the required rules.
    /// </remarks>

    public class CreateProductCommand : IRequest<ProductResult>
    {/// <summary>
     /// Gets or sets the title of the product.
     /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the image URL of the product.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets the product associated with this sale item.
        /// </summary>
        public ProductRating Rating { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new CreateProductCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
