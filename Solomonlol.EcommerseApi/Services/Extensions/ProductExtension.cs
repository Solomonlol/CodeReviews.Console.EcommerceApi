using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Services.Extensions.Filters;
using Solomonlol.EcommerseApi.Services.Extensions.Sort;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Solomonlol.EcommerseApi.Services.Extensions
{
    public static class ProductExtension
    {
        public static IQueryable<Product> Filter(this IQueryable<Product> query, ProductFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name == filter.Name);

            if (filter.MaxPrice >= 0)
                query = query.Where(x => x.Price <= filter.MaxPrice);

            if (filter.MinPrice >= 0)
                query = query.Where(x => x.Price >= filter.MinPrice);

            return query;
        }

        public static IQueryable<Product> Sort(this IQueryable<Product> query, SortParams sortParams)
        {
            return sortParams.Direction == ListSortDirection.Descending
                ? query.OrderByDescending(GetKeySelector(sortParams.OrderBy))
                : query.OrderBy(GetKeySelector(sortParams.OrderBy));
        }

        private static Expression<Func<Product, object>> GetKeySelector(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                return x => x.Name;

            return orderBy switch
            {
                nameof(Product.Price) => c => c.Price,
                nameof(Product.Category) => c => c.Category.Name,
                _ => c => c.Name,
            };
        }
    }
}
