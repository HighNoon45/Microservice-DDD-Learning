using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepo _articleRepo;

        public ArticleService(IArticleRepo articleRepo)
        {
            _articleRepo = articleRepo;
        }

        public async Task<Article> GetByIdAsync(Guid id)
        {
            return await _articleRepo.GetByIdAsync(id);
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await _articleRepo.GetAllAsync();
        }

        public async Task<Article> CreateAsync(Article article)
        {
            return await _articleRepo.AddAsync(article);
        }

        public async Task<Article> UpdateAsync(Article article)
        {
            var existing = await _articleRepo.GetByIdAsync(article.Id);

            return await _articleRepo.UpdateAsync(article);
        }

        public async Task<bool> RemoveAsync(Article article)
        {
            var entity = await _articleRepo.GetByIdAsync(article.Id);

            return await _articleRepo.RemoveAsync(entity);
        }
    }
}