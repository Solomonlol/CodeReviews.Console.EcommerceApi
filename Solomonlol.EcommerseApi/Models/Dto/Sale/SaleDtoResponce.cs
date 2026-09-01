namespace Solomonlol.EcommerseApi.Models.Dto.Sale
{
    public class SaleDtoResponce : SaleDtoRequest
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsEnded { get; set; } = false;
        public DateTime? EndedAt { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}
