using System.ComponentModel;

namespace EcommerseAPI.Frontend.Entities.Sort
{
    public class SortParams
    {
        public string? OrderBy { get; set; }
        public ListSortDirection? Direction { get; set; }
    }
}
