using Ardalis.GuardClauses;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PricingAggregate
{
    public class Pricing : BaseEntity, IAggregateRoot
    {
        private Pricing() { }
        public  Guid ArticleId { get; private set; }
        private readonly List<Cost> _costs = new List<Cost>();
        private readonly List<Margin> _margins = new List<Margin>();
        private readonly List<Markup> _markups = new List<Markup>();
        public IReadOnlyCollection<Cost> Costs => _costs.AsReadOnly();
        public IReadOnlyCollection<Margin> Margins => _margins.AsReadOnly();
        public IReadOnlyCollection<Markup> Markups => _markups.AsReadOnly();
        public bool IsDeleted { get; private set; } = false;
        
        public Pricing (Guid articleId)
        {
            ArticleId = articleId;
        }

        public void AddCost(Cost cost)
        {
            if (Costs.Any(x => x.Value == cost.Value || x.ValidFrom == cost.ValidFrom || x.ValidTo == cost.ValidTo))
                return;
            _costs.Add(cost);
        }

        public void AddMargin(Margin margin)
        {
            if (Margins.Any(x => x.Percentage == margin.Percentage || x.ValidFrom == margin.ValidFrom || x.ValidTo == margin.ValidTo))
                return;
            _margins.Add(margin); 
        }

        public void AddMarkup(Markup markup)
        {
            if (Markups.Any(x => x.Percentage == markup.Percentage || x.ValidFrom == markup.ValidFrom || x.ValidTo == markup.ValidTo))
                return;
                _markups.Add(markup);
        }

        public void UpdateCost(Cost cost)
        {
            var entity = _costs.FirstOrDefault(x => x.Id == cost.Id);

            if(entity == default)
                return;

            entity.Delete();
            _costs.Add(cost);
        }

        public void UpdateMargin(Margin margin)
        {
            var entity = _margins.FirstOrDefault(x => x.Id == margin.Id);
            if (entity == default)
                return;
                
            entity.Delete();
            _margins.Add(margin);
        }

        public void UpdateMarkup(Markup markup) // FirstOrDefault
        {
            var entity = _markups.FirstOrDefault(x => x.Id == markup.Id);
            if(entity == default)
                return;

            entity.Delete();
            _markups.Add(markup);
        }

        public void RemoveCost(Cost cost)
        {
            if (Costs.Any(x => x.Id == cost.Id)) 
            {
                _costs.First(x => x.Id == cost.Id).Delete();
            }
        }

        public void RemoveMargin(Margin margin)
        {
            if (Margins.Any(x => x.Id == margin.Id))
            {
                _margins.First(x => x.Id == margin.Id).Delete();
            }
        }

        public void RemoveMarkup(Markup markup)
        {
            if (Markups.Any(x => x.Id == markup.Id))
            {
                _markups.First(x => x.Id == markup.Id).Delete();
                return;
            }
        }

        public void OrderAllPrincipals()
        {
            _costs.OrderBy(c => c.ValidFrom);
            _margins.OrderBy(m => m.ValidFrom);
            _markups.OrderBy(m => m.ValidFrom);
        }

        public void Remove() 
        { 
            IsDeleted = true;
        }
    }
}
