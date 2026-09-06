namespace Solomonlol.EcommerseApi.Models.Dto.Product
{
    public class ProductAttributeValueDto
    {
        public int ProductId { get; set; }

        public int ProductAttributeId { get; set; }
        public string Value { get; set; } = null!;
    }
}
