namespace Solomonlol.EcommerseApi.Models.Base
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string>? ProductProperties { get; set; }

    }
}
