using System.ComponentModel.DataAnnotations.Schema;

namespace Solomonlol.EcommerseApi.Models.Dto.Sale
{
    public class SaleItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
