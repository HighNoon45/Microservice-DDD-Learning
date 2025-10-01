using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IArticleRepo
    {
        Task<Article> GetByIdAsync(Guid id);
        Task<List<Article>> GetAllAsync();
        Task<Article> AddAsync(Article entity);
        Task<Article> UpdateAsync(Article entity);
        Task<bool> RemoveAsync(Article entity);
    }
}
