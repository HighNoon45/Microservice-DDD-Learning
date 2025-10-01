using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IPricingRepo
    {
        Task<Pricing> GetByIdAsync(Guid id);
        Task<Pricing> GetByArticleIdAndDateAsync(Guid articleId, DateTime date);
        Task<Pricing> GetByArticleId(Guid articleId);
        Task<List<Pricing>> GetAllAsync();
        Task<Pricing> AddAsync(Pricing entity);
        Task<Pricing> UpdateAsync(Pricing entity);
        Task<bool> RemoveAsync(Pricing entity);
    }
}
