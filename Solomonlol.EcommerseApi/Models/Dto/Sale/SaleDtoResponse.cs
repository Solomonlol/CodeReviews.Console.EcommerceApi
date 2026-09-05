using Solomonlol.EcommerseApi.Models.Dto.User;

namespace Solomonlol.EcommerseApi.Models.Dto.Sale
{
    public class SaleDtoResponse
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsEnded { get; set; } = false;
        public DateTime? EndedAt { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        //public UserDtoResponse User { get; set; } = null!;
        public int UserId { get; set; }
        public ICollection<SaleItemDtoResponse> SaleItems { get; set; } = [];
    }
}
