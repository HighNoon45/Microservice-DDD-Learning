namespace Presentation.DTOs
{
    public class PricingDto
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public List<CostDto> Costs { get; set; } = new List<CostDto>();
        public List<MarginDto> Margins { get; set; } = new List<MarginDto>();
        public List<MarkupDto> Markups { get; set; } = new List<MarkupDto>();
    }
}
