namespace Solomonlol.EcommerseApi.Models.Dto.Sale
{
    public class SaleDtoRequest
    {
        public int UserId { get; set; }
        public ICollection<SaleItemDto> SaleItems { get; set; } = [];
    }
}
