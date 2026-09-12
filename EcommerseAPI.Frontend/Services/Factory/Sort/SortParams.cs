using System.ComponentModel;

namespace EcommerseAPI.Frontend.Services.Factory.Sort
{
    public class SortParams
    {
        public string? OrderBy { get; set; }
        public ListSortDirection? Direction { get; set; }
    }
}
