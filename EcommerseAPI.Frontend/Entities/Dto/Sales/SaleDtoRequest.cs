using EcommerseAPI.Frontend.Interfaces;

namespace EcommerseAPI.Frontend.Entities.Dto.Sales
{
    public class SaleDtoRequest
    {
        public int UserId { get; set; }
        public ICollection<SaleItemDtoRequest> SaleItems { get; set; } = [];
    }
}
