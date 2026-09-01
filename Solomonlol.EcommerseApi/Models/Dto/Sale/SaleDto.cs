namespace Solomonlol.EcommerseApi.Models.Dto.Sale
{
    public class SaleDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsEnded { get; set; } = false;
        public DateTime? EndedAt { get; set; } = null!;
        public ICollection<SaleItemDto> SaleItems { get; set; } = [];
    }
}
