namespace Solomonlol.EcommerseApi.Models.Dto.Product
{
    public class ProductAttributeValueDto
    {
        public int ProductId { get; set; }

        public string ProductAttributeName { get; set; } = string.Empty;
        public string Value { get; set; } = null!;
    }
}
