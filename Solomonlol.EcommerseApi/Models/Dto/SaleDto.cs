namespace Solomonlol.EcommerseApi.Models.Dto
{
    public class SaleDto
    {
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsEnded { get; set; } = false;
        public DateTime? EndedAt { get; set; } = null!;
        public int UserId { get; set; }
    }
}
