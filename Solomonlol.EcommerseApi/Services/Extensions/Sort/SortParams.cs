using System.ComponentModel;

namespace Solomonlol.EcommerseApi.Services.Extensions.Sort
{
    public class SortParams
    {
        public string? OrderBy { get; set; }
        public ListSortDirection? Direction { get; set; }
    }
}
