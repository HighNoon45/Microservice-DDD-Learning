using Microsoft.EntityFrameworkCore;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class ArticleRepo : IArticleRepo
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Article> _dbSet;
        private const string EntityName = nameof(Article); 

        public ArticleRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Article>();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(EntityName, id);

            return entity;
        }

        public async Task<List<Article>> GetAllAsync() =>
            await _dbSet.ToListAsync();

        public async Task<Article> AddAsync(Article entity)
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

        public async Task<Article> UpdateAsync(Article entity)
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

        public async Task<bool> RemoveAsync(Article entity)
        {
            try
            {
                _dbSet.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new EntityRemoveException(EntityName, ex);
            }
        }
    }
}
