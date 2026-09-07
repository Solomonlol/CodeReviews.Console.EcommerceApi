using Solomonlol.EcommerseApi.Models.Base;
using Solomonlol.EcommerseApi.Services.Extensions.Filters;
using Solomonlol.EcommerseApi.Services.Extensions.Sort;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Solomonlol.EcommerseApi.Services.Extensions
{
    public static class UserExtension
    {
        public static IQueryable<User> Filter(this IQueryable<User> query, UserFilter filter)
        {
            if (filter.Id != null)
                query = query.Where(x => x.Id.Equals(filter.Id));

            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(x => x.Email == filter.Email);

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
                query = query.Where(x => x.PhoneNumber == filter.PhoneNumber);

            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(x => x.FirstName == filter.FirstName);

            if (!string.IsNullOrEmpty(filter.LastName))
                query = query.Where(x => x.LastName == filter.LastName);

            if (!string.IsNullOrEmpty(filter.Login))
                query = query.Where(x => x.Login == filter.Login);

            return query;
        }

        public static IQueryable<User> Sort(this IQueryable<User> query, SortParams sortParams)
        {
            return sortParams.Direction == ListSortDirection.Descending
                ? query.OrderByDescending(GetKeySelector(sortParams.OrderBy))
                : query.OrderBy(GetKeySelector(sortParams.OrderBy));
        }

        private static Expression<Func<User, object>> GetKeySelector(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                return u => u.Id;

            return orderBy switch
            {
                nameof(User.Login) => u => u.Login,
                nameof(User.Email) => u => u.Email,
                nameof(User.FirstName) => u => u.FirstName,
                nameof(User.LastName) => u => u.LastName,
                nameof(User.PhoneNumber) => u => u.PhoneNumber,
                nameof(User.Role) => u => u.Role,
                _ => u => u.Id,
            };
        }
    }
}
