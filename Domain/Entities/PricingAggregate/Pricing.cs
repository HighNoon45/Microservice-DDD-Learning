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
        public  int ArticleId { get; private set; }
        private readonly List<Cost> _costs = new List<Cost>();
        private readonly List<Margin> _margins = new List<Margin>();
        private readonly List<Markup> _markups = new List<Markup>();
        public IReadOnlyCollection<Cost> Costs => _costs.AsReadOnly();
        public IReadOnlyCollection<Margin> Margins => _margins.AsReadOnly();
        public IReadOnlyCollection<Markup> Markups => _markups.AsReadOnly();
        public bool IsDeleted { get; private set; } = false;
        
        public Pricing (int articleId)
        {
            ArticleId = articleId;
        }

        public void AddCost(Cost cost)
        {
            if(!Costs.Any(x => x.Id == cost.Id))
            {
                _costs.Add(cost);
            }
        }

        public void AddMargin(Margin margin)
        {
            if (!Margins.Any(x => x.Id == margin.Id))
            {
                _margins.Add(margin); 
            }
        }

        public void AddMarkup(Markup markup)
        {
            if (!Markups.Any(x => x.Id == markup.Id))
            {
                _markups.Add(markup);
            }
        }

        public void UpdateCost(Cost cost)
        {
            if (Costs.Any(x => x.Id == cost.Id))
            {
                _costs.First(x => x.Id == cost.Id).Delete();
                _costs.Add(cost);
            }
        }

        public void UpdateMargin(Margin margin)
        {
            if (Margins.Any(x => x.Id == margin.Id))
            {
                _margins.First(x => x.Id == margin.Id).Delete();
                _margins.Add(margin);
            }
        }

        public void UpdateMarkup(Markup markup) // FirstOrDefault
        {
            if (Markups.Any(x => x.Id == markup.Id))
            {
                _markups.First(x => x.Id == markup.Id).Delete();
                _markups.Add(markup);
            }
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

        public void Remove() 
        { 
            IsDeleted = true;
        }
    }
}
