using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Services.Extensions.Filters;
using Solomonlol.EcommerseApi.Services.Extensions.Sort;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Solomonlol.EcommerseApi.Services.Extensions
{
    public static class CategoryExtension
    {
        public static IQueryable<Category> Filter(this IQueryable<Category> query, CategoryFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name == filter.Name);

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(x => x.Description == filter.Description);

            return query;
        }

        public static IQueryable<Category> Sort(this IQueryable<Category> query, SortParams sortParams)
        {
            return sortParams.Direction == ListSortDirection.Descending
                ? query.OrderByDescending(GetKeySelector(sortParams.OrderBy))
                : query.OrderBy(GetKeySelector(sortParams.OrderBy));
        }

        private static Expression<Func<Category, object>> GetKeySelector(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                return x => x.Name;

            return orderBy switch
            {
                nameof(Category.Description) => c => c.Description,
                _ => c => c.Name,
            };
        }
    }
}
