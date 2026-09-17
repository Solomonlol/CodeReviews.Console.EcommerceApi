using EcommerseAPI.Frontend.Interfaces;

namespace EcommerseAPI.Frontend.Entities.Dto.Sales
{
    public class SaleItemDtoRequest
    {
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
