using Solomonlol.EcommerseApi.Models.Dto.User;

namespace Solomonlol.EcommerseApi.Models.Dto.Sale
{
    public class SaleDtoResponce
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsEnded { get; set; } = false;
        public DateTime? EndedAt { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public UserDtoResponce User { get; set; } = null!;
        public ICollection<SaleItemDtoResponce> SaleItems { get; set; } = [];
    }
}
