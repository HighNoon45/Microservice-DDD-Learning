using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public static class PricingQueries
    {
        public static Func<DbSet<Pricing>,IQueryable<Pricing>> IncludeAllWithArtticleIdAndOnDate(Guid articleId, DateTime date)
        {
            return set => set
            .AsSplitQuery()
            .Where(p => p.ArticleId == articleId)
            .Include(m => m.Costs.Where(x => x.ValidFrom >= date && x.ValidTo >= date))
            .Include(m => m.Margins.Where(x => x.ValidFrom >= date && x.ValidTo >= date))
            .Include(m => m.Markups.Where(x => x.ValidFrom >= date && x.ValidTo >= date));
        }

        public static Func<DbSet<Pricing>, IQueryable<Pricing>> IncludeAll =>
            set => set
            .AsSplitQuery()
            .Include(c => c.Costs)
            .Include(m => m.Margins)
            .Include(h => h.Markups);

        public static Func<DbSet<Pricing>, IQueryable<Pricing>> IncludeAllWithArticleId(Guid articleId)
        {
            return set => set
            .AsSplitQuery()
            .Where(p => p.ArticleId == articleId)
            .Include(m => m.Costs)
            .Include(m => m.Margins)
            .Include(m => m.Markups);
        }
    }
}
