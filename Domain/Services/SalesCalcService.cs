using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public static class SalesCalcService
    {
            //public static SalesCalcService Calculate(PriceCompositeVO priceCompositeVO)
            //{
            //    var priceFloor = SetPriceFloor(priceCompositeVO.AcquisitionCost.PurchasePrice, priceCompositeVO.Margin.MarginPercentage);
            //    var retailPrice = SetRetailPrice(priceFloor, priceCompositeVO.Markup.MarkupPercentage);
            //    var discountCap = SetDiscountCap(retailPrice, priceFloor);

            //    return (new SalesInfoValueObject(priceFloor, retailPrice, discountCap));
            //}

            //private static decimal SetPriceFloor(decimal acquititionCostPrice, decimal marginPercentage)
            //{
            //    marginPercentage = marginPercentage / 100; // Convert percentage to decimal
            //    return acquititionCostPrice * (1 + marginPercentage);

            //}

            //private static decimal SetRetailPrice(decimal priceFloor, decimal markupPercentage)
            //{
            //    markupPercentage = markupPercentage / 100; // Convert percentage to decimal
            //    return priceFloor * (1 + markupPercentage);
            //}

            //private static decimal SetDiscountCap(decimal retailPrice, decimal priceFloor)
            //{
            //    return (retailPrice - priceFloor) / retailPrice;
            //}
    }
}
