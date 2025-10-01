using Domain.Entities.PricingAggregate;
using Domain.Exceptions;
using Domain.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;

namespace Presentation.Controllers
{
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly IPricingService _pricingService;
        public PricingController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        [Route("api/pricings/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var pricing = await _pricingService.GetByIdAsync(id);
            return Ok(pricing.Adapt<PricingDto>());
        }

        [Route("api/pricings/latest/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetLatestById(Guid id)
        {
            var pricing = await _pricingService.GetByIdAsync(id);
            
            return Ok(pricing.Adapt<PricingDto>());
        }

        [Route("api/pricings/article/{articleId}/date/{date}")]
        [HttpGet]
        public async Task<IActionResult> GetByArticleIdAndDate(Guid articleId, DateTime date)
        {
            var pricing = await _pricingService.GetByArticleIdAndDateAsync(articleId, date);
            return Ok(pricing.Adapt<PricingDto>());
        }

        [Route("api/pricings")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pricings = await _pricingService.GetAllAsync();
            return Ok(pricings.Adapt<PricingDto>());
        }

        [Route("api/pricings")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PricingDto pricing)
        {
            var createdPricing = await _pricingService.AddAsync(pricing.Adapt<Pricing>());
            return CreatedAtAction(nameof(GetById), new { id = createdPricing.Id }, createdPricing.Adapt<PricingDto>());
        }

        [Route("api/pricings/sync-costs")]
        [HttpPut]
        public async Task<IActionResult> SyncCosts([FromBody] PricingDto pricing)
        {
            var updatedPricing = await _pricingService.SyncCostsAsync(pricing.Adapt<Pricing>());
            return Ok(updatedPricing.Adapt<PricingDto>());
        }

        [Route("api/pricings/sync-margins")]
        [HttpPut]
        public async Task<IActionResult> SyncMargins([FromBody] PricingDto pricing)
        {
            var updatedPricing = await _pricingService.SyncMarginsAsync(pricing.Adapt<Pricing>());
            return Ok(updatedPricing.Adapt<PricingDto>());
        }

        [Route("api/pricings/sync-markups")]
        [HttpPut]
        public async Task<IActionResult> SyncMarkups([FromBody] PricingDto pricing)
        {
            var updatedPricing = await _pricingService.SyncMarkupsAsync(pricing.Adapt<Pricing>());
            return Ok(updatedPricing.Adapt<PricingDto>());
        }

        [Route("api/pricings/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Remove(Guid id)
        {
            var pricing = await _pricingService.GetByIdAsync(id);
            if (pricing == null)
            {
                return NotFound();
            }
            var result = await _pricingService.RemoveAsync(pricing);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "An error occurred while deleting the pricing.");
            }
        }

        [Route("api/Salesprice")]
        [HttpPost]
        public async Task<IActionResult> ValidateByArticleIdPriceAndDiscount([FromBody] Guid articleId, decimal cost, decimal discount)
        {
            try
            {
                var pricing = await _pricingService.GetByIdAsync(articleId);
                pricing.OrderAllPrincipals();

                var salesPrice = SalesCalcService.Calculate(pricing.Costs.First(), pricing.Margins.First(), pricing.Markups.First());

                if (salesPrice.Error != null)
                {
                    return StatusCode(salesPrice.Error.Returncode, salesPrice.Error);
                }

                if(discount > salesPrice.Value.DiscountCap)
                {
                    return StatusCode(ResultErrors.DiscountGreaterThanMaxDiscount.Returncode, ResultErrors.DiscountGreaterThanMaxDiscount);
                }
                else if (cost < salesPrice.Value.PriceFloor)
                {
                    return StatusCode(ResultErrors.PriceLowerThanLimit.Returncode, ResultErrors.PriceLowerThanLimit);
                }

                if(cost < salesPrice.Value.RetailPrice)
                {
                   return StatusCode(ResultWarnings.PriceLowerThanSalesPrice.Returncode, ResultWarnings.PriceLowerThanSalesPrice);
                }

                return Ok("Looks alright");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
