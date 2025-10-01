using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public static class SalesCalcService
    {
        public readonly record struct PricingComponents(decimal PriceFloor, decimal RetailPrice, decimal DiscountCap);
        public static Result<PricingComponents> Calculate(Cost cost, Margin margin, Markup markup)
        {
            if (cost.Value <= 0)
                return ResultErrors.ZeroCostDivision;

            var priceFloor = SetPriceFloor(cost.Value, margin.Percentage);
            var retailPrice = SetRetailPrice(priceFloor, markup.Percentage);
            var discountCap = SetDiscountCap(retailPrice, priceFloor);

            return new PricingComponents(priceFloor, retailPrice, discountCap);
        }

        private static decimal SetPriceFloor(decimal costValue, decimal marginPercentage)
        {  
            marginPercentage = marginPercentage / 100; // Convert percentage to decimal
            return costValue * (1 + marginPercentage);   
        }

        private static decimal SetRetailPrice(decimal priceFloor, decimal markupPercentage)
        {
            markupPercentage = markupPercentage / 100; // Convert percentage to decimal
            return priceFloor * (1 + markupPercentage);
        }

        private static decimal SetDiscountCap(decimal retailPrice, decimal priceFloor)
        {
            return (retailPrice - priceFloor) / retailPrice;
        }
    }
}
