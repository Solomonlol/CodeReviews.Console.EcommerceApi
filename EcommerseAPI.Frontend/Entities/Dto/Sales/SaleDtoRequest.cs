using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Entities.Dto.Sales
{
    public class SaleDtoRequest
    {
        public int UserId { get; set; }
        public ICollection<SaleItemDtoRequest> SaleItems { get; set; } = [];
    }
}
