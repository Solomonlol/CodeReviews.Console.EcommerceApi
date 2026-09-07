using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Services.Extensions.Filters;
using Solomonlol.EcommerseApi.Services.Extensions.Sort;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Solomonlol.EcommerseApi.Services.Extensions
{
    public static class SaleExtension
    {
        public static IQueryable<Sale> Filter(this IQueryable<Sale> query, SaleFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.CreatedAt))
                query = query.Where(x => x.CreatedAt.ToShortDateString().ToString().Contains(filter.CreatedAt));

            if (filter.MaxTotalPrice >= 0)
                query = query.Where(x => x.TotalPrice <= filter.MaxTotalPrice);

            if (filter.MinTotalPrice >= 0)
                query = query.Where(x => x.TotalPrice >= filter.MinTotalPrice);

            return query;
        }

        public static IQueryable<Sale> Sort(this IQueryable<Sale> query, SortParams sortParams)
        {
            return sortParams.Direction == ListSortDirection.Descending
                ? query.OrderByDescending(GetKeySelector(sortParams.OrderBy))
                : query.OrderBy(GetKeySelector(sortParams.OrderBy));
        }

        private static Expression<Func<Sale, object>> GetKeySelector(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                return x => x.Id;

            return orderBy switch
            {
                nameof(Sale.CreatedAt) => c => c.CreatedAt,
                nameof(Sale.EndedAt) => c => c.EndedAt,
                nameof(Sale.User) => c => c.User.Login,
                _ => c => c.Id,
            };
        }
    }
}
