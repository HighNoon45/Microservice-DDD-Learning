using Ardalis.GuardClauses;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PricingAggregate
{
    public class Margin : BaseEntity, IAggregateRoot, IPricingPrincipal
    {
        private Margin() { }
        public decimal Percentage { get; init; }
        public DateTime ValidFrom { get; init; }
        public DateTime ValidTo { get; init; }
        public int PricingId { get; private set; }
        public bool IsDeleted { get; private set; } = false;


        public Margin(decimal percentage, DateTime validFrom, DateTime validTo)
        {
            Guard.Against.OutOfRange(Percentage, nameof(Percentage), -500, 500);
            Guard.Against.NullOrOutOfRange(ValidFrom, nameof(ValidFrom), DateTime.MinValue, ValidTo);
            Guard.Against.NullOrOutOfRange(ValidTo, nameof(ValidTo), ValidFrom, DateTime.MaxValue);
            Percentage = percentage;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
