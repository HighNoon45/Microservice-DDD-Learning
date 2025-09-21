using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public static class CostFilters
    {
        public static Expression<Func<IQueryable<Cost>,IQueryable<Cost>>> IsValidOn(DateTime date)
        {
            return u => u.Where(x => x.ValidFrom >= date && x.ValidTo >= date);
        }
    }
}
