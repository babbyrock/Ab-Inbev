using Ambev.DeveloperEvaluation.Application.Carts.GetCarts;
using FluentValidation;
using System;

namespace Ambev.DeveloperEvaluation.Application.Carts
{
    public class GetCartsValidator : AbstractValidator<GetCartsCommand>
    {
        public GetCartsValidator()
        {
            RuleFor(x => x)
                .Must(x => IsValidDateRange(x.MinDate, x.MaxDate))
                .WithMessage("A data mínima não pode ser maior que a data máxima.");
        }

        private bool IsValidDateRange(DateTime? minDate, DateTime? maxDate)
        {
            return !(minDate.HasValue && maxDate.HasValue && minDate > maxDate);
        }
    }
}
