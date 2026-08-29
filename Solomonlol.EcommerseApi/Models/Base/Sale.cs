using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solomonlol.EcommerseApi.Models.Base
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (var item in SaleItems)
                {
                    total += item.UnitPrice * item.Quantity;
                }
                return total;
            }
        }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
        public ICollection<SaleItem> SaleItems { get; set; } = [];
    }
}
