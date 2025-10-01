using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IPricingService
    {
        Task<Pricing> GetByIdAsync(Guid id);
        Task<Pricing> GetByArticleIdAsync(Guid articleId);
        Task<Pricing> GetByArticleIdAndDateAsync(Guid articleId, DateTime date);
        Task<List<Pricing>> GetAllAsync();
        Task<Pricing> AddAsync(Pricing entity);
        Task<Pricing> SyncCostsAsync(Pricing newPricing);
        Task<Pricing> SyncMarginsAsync(Pricing newPricing);
        Task<Pricing> SyncMarkupsAsync(Pricing newPricing);
        Task<bool> RemoveAsync(Pricing pricing);
    }
}
