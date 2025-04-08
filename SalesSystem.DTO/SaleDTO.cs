namespace SalesSystem.DTO
{
    public class SaleDTO
    {
        public int IdSale { get; set; }

        public string? IdNumber { get; set; }

        public string? PaymentType { get; set; }

        public string? TotalText { get; set; }

        public string? Timestamp { get; set; }
        public virtual ICollection<SaleDetailsDTO> SaleDetails { get; set; } = new List<SaleDetailsDTO>();
    }
}
