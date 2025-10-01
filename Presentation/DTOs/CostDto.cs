namespace Presentation.DTOs
{
    public class CostDto
    {
        public readonly Guid Id = default;
        public decimal Percentage;
        public DateTime ValidFrom;
        public DateTime ValidTo;
    }
}
