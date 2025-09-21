using Microsoft.EntityFrameworkCore;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Specifications;
using Infrastructure.Queries;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class PricingRepo : IPricingRepo
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Pricing> _dbSet;
        private const string EntityName = nameof(Pricing); 

        public PricingRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Pricing>();
        }

        public async Task<Pricing> GetByIdAsync(int id)
        {
            var entity = await PricingQueries.IncludeAll(_dbSet).FirstOrDefaultAsync( x => x.Id == id);
            if (entity == null)
                throw new EntityNotFoundException(EntityName, id);

            return entity;
        }

        public async Task<IEnumerable<Pricing>> GetByArticleIdAndDateAsync(int articleId, DateTime date)
        {
            var entity = await PricingQueries.IncludeAllWithArtticleIdAndOnDate(articleId, date)(_dbSet).ToListAsync();
            if (entity == null)
                throw new EntityNotFoundException(EntityName, articleId, date);

            return entity;
        }

        public async Task<List<Pricing>> GetAllAsync()
        {
            return await PricingQueries.IncludeAll(_dbSet).ToListAsync();
        }

        public async Task<Pricing> AddAsync(Pricing entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new EntityAddException(EntityName, ex);
            }
        }

        public async Task<Pricing> UpdateAsync(Pricing entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new EntityUpdateException(EntityName, ex);
            }
        }

        public async Task<bool> RemoveAsync(Pricing entity) //Could have a non tracked entity, also fix this layer violation
        {
            try
            {
                if (await _dbSet.AnyAsync(x => x.Id == entity.Id))
                {
                    foreach (var item in entity.Costs)
                    {
                        entity.RemoveCost(item);
                    }
                    foreach (var item in entity.Margins)
                    {
                        entity.RemoveMargin(item);
                    }
                    foreach (var item in entity.Markups)
                    {
                        entity.RemoveMarkup(item);
                    }
                    entity.Remove();
                }

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new EntityRemoveException(EntityName, ex);
            }
        }
    }
}
