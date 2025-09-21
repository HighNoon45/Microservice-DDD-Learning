using Domain.Entities; // Assuming Article is in Domain.Models
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IArticleService
    {
        Task<Article> GetByIdAsync(int id);
        Task<List<Article>> GetAllAsync();
        Task<Article> CreateAsync(Article article);
        Task<Article> UpdateAsync(Article article);
        Task<bool> DeleteAsync(int id);
    }
}
