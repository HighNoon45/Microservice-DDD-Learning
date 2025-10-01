using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PricingService : IPricingService
    {
        private readonly IPricingRepo _pricingRepo;

        public PricingService(IPricingRepo pricingRepo)
        {
            _pricingRepo = pricingRepo;
        }

        public async Task<Pricing> GetByIdAsync(Guid id)
        {
            return await _pricingRepo.GetByIdAsync(id);
        }

        public async Task<Pricing> GetByArticleIdAsync(Guid articleId)
        {
            return await _pricingRepo.GetByArticleId(articleId);
        }

        public async Task<Pricing> GetByArticleIdAndDateAsync(Guid articleId, DateTime date)
        {
            return await _pricingRepo.GetByArticleIdAndDateAsync(articleId, date);
        }

        public async Task<List<Pricing>> GetAllAsync()
        {
            return await _pricingRepo.GetAllAsync();
        }

        public async Task<Pricing> AddAsync(Pricing entity)
        {
            return await _pricingRepo.AddAsync(entity);
        }

        public async Task<Pricing> SyncCostsAsync(Pricing newPricing)
        {
            var pricing = await _pricingRepo.GetByIdAsync(newPricing.Id);

            CollectionSync.SyncCollections(
                pricing.Costs,
                newPricing.Costs,
                c => c.Id,
                pricing.RemoveCost,
                pricing.AddCost,
                (existing, incoming) =>
                {
                    pricing.UpdateCost(incoming);
                }
            );

            return await _pricingRepo.UpdateAsync(pricing);
        }

        public async Task<Pricing> SyncMarginsAsync(Pricing newPricing)
        {
            var pricing = await _pricingRepo.GetByIdAsync(newPricing.Id);

            CollectionSync.SyncCollections(
                pricing.Margins,
                newPricing.Margins,
                m => m.Id,
                pricing.RemoveMargin,
                pricing.AddMargin,
                (existing, incoming) =>
                {
                    pricing.UpdateMargin(incoming);
                }
            );

            return await _pricingRepo.UpdateAsync(pricing);
        }

        public async Task<Pricing> SyncMarkupsAsync(Pricing newPricing)
        {
            var pricing = await _pricingRepo.GetByIdAsync(newPricing.Id);

            CollectionSync.SyncCollections(
                pricing.Markups,
                newPricing.Markups,
                m => m.Id,
                pricing.RemoveMarkup,
                pricing.AddMarkup,
                (existing, incoming) =>
                {
                    pricing.UpdateMarkup(incoming);
                }
            );

            return await _pricingRepo.UpdateAsync(pricing);
        }

        public async Task<bool> RemoveAsync(Pricing pricing)
        {
            return await _pricingRepo.RemoveAsync(pricing);
        }
    }
}
