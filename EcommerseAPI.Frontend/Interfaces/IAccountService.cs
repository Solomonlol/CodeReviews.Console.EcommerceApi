using EcommerseAPI.Frontend.Entities.Dto.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IAccountService
    {
        Task GetMe(CancellationToken ct = default);
        Task GetOne(string login, CancellationToken ct = default);
        Task GetAll(CancellationToken ct = default);
        Task Create(UserDtoCreation user, CancellationToken ct = default);
        Task Update(UserDtoRequest user, CancellationToken ct = default);
        Task Delete(string login, CancellationToken ct = default);
    }
}
