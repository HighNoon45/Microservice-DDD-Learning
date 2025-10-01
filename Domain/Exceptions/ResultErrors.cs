using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public enum ErrorType { NotFound, Validation, Unauthorized }

    public record Error(string ErrorKey, ErrorType Type, string Description, int Returncode);
    public static class ResultErrors
    {
        public static Error ZeroCostDivision { get; } = new("ZeroCostDivision", ErrorType.Validation, "Cost cannot be zero", 501);
        public static Error DiscountGreaterThanMaxDiscount { get; } = new("DiscountGreaterThanMaxDiscount", ErrorType.Validation, "Discount is greater thatn the maximum allowed Discount", 502);
        public static Error PriceLowerThanLimit { get; } = new("PriceLowerThanLimit", ErrorType.Validation, "Price is lower than the limit price", 503);
    }
}
