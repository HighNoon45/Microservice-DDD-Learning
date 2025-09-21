using Ardalis.GuardClauses;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PricingAggregate
{
    public class Cost : BaseEntity, IAggregateRoot, IPricingPrincipal
    {
        protected Cost() { }
        public decimal Value { get; init; }
        public DateTime ValidFrom { get; init; }
        public DateTime ValidTo { get; init; }
        public int PricingId { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public Cost(decimal value, DateTime validFrom, DateTime validTo)
        {
            Guard.Against.NegativeOrZero(Value);
            Guard.Against.NullOrOutOfRange(ValidFrom, nameof(ValidFrom), DateTime.MinValue, ValidTo);
            Guard.Against.NullOrOutOfRange(ValidTo, nameof(ValidTo), ValidFrom, DateTime.MaxValue);
            Value = value;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        public void Delete() 
        { 
            IsDeleted = true;
        }
    }
}
