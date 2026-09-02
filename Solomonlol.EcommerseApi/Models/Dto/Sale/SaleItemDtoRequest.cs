using System.ComponentModel.DataAnnotations.Schema;

namespace Solomonlol.EcommerseApi.Models.Dto.Sale
{
    public class SaleItemDtoRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
