using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public enum WarningType { Deprecated, Performance, Security, IrregularValue }

    public record Warning(string WarningKey, WarningType Type, string Description, int Returncode);
    public static class ResultWarnings
    {
        public static Warning PriceLowerThanSalesPrice = new Warning("PriceLowerThanSalesPrice", WarningType.IrregularValue, "The entered price is lower than the sales price.", 1001);

    }
    
}