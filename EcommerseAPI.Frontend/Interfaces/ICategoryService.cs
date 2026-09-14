using EcommerseAPI.Frontend.Entities.Dto.Categories;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface ICategoryService : IPagedResultService
    {
        
        Task GetOne(string categoryName, CancellationToken ct = default);
        Task Create(CategoryDto user, CancellationToken ct = default);
        Task Update(CategoryDto user, CancellationToken ct = default);
        Task Delete(string categoryName, CancellationToken ct = default);
    }
}
