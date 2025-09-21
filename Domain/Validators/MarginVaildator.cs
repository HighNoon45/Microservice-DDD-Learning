using Domain.Entities.PricingAggregate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class MarginVaildator : AbstractValidator<Margin>
    {
        public MarginVaildator() 
        {
            RuleFor(x => x.Percentage)
                .NotNull().WithMessage("Margin percentage must be provided.")
                .InclusiveBetween(-500, 500).WithMessage("Margin percentage must be between -500 and 500.");
            RuleFor(x => x.ValidFrom)
                .LessThan(x => x.ValidTo).WithMessage("Start date must be earlier than end date.");
        }
    }
}
