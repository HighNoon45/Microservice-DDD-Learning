using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public static class MarkupFilters
    {
        public static Expression<Func<Cost,bool>> IsValidOn(DateTime date)
        {
            return u => u.ValidFrom >= date && u.ValidTo >= date;
        }
    }
}
