namespace Solomonlol.EcommerseApi.Models.Dto.Sale
{
    public class SaleDtoRequest
    {
        public int UserId { get; set; }
        public ICollection<SaleItemDtoRequest> SaleItems { get; set; } = [];
    }
}
