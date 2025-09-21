using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public static class MarginFilters
    {
        public static Expression<Func<Cost,bool>> IsValidOn(DateTime date)
        {
            return u => u.ValidFrom >= date && u.ValidTo >= date;
        }
    }
}
