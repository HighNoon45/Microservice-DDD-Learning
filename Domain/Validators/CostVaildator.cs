using Domain.Entities.PricingAggregate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class CostVaildator : AbstractValidator<Cost>
    {
        public CostVaildator() 
        {
            RuleFor(x => x.Value)
                .NotNull().WithMessage("Margin percentage must be provided.")
                .GreaterThan(0).WithMessage("Purchase price must be greater than zero.");
            RuleFor(x => x.ValidFrom)
                .LessThan(x => x.ValidTo).WithMessage("Start date must be earlier than end date.");
        }
    }
}
