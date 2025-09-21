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

        public async Task<Pricing> GetByIdAsync(int id)
        {
            return await _pricingRepo.GetByIdAsync(id);
        }

        public async Task<Pricing> GetLatestByIdAsync(int id)
        {
            var entity = await _pricingRepo.GetByIdAsync(id);
            entity.Costs.OrderBy(x => x.ValidFrom);
            entity.Margins.OrderBy(x => x.ValidFrom);
            entity.Markups.OrderBy(x => x.ValidFrom);

            return entity;
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
                remove => pricing.RemoveCost(remove),
                add => pricing.AddCost(add),
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
                remove => pricing.RemoveMargin(remove),
                add => pricing.AddMargin(add),
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
                remove => pricing.RemoveMarkup(remove),
                add => pricing.AddMarkup(add),
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
