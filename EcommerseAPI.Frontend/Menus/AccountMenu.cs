using EcommerseAPI.Frontend.Entities;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services;
using EcommerseAPI.Frontend.Services.Factory.Filters;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Menus
{
    internal class AccountMenu : UserInterface
    {
        private readonly IAccountService _accountService;
        private readonly ILoginService _loginService;
        //private readonly PagedMenu<AccountService, UserDtoResponse, UserFilter> _pagedMenu;

        public AccountMenu(IAccountService accountService, ILoginService loginService, IServiceProvider sp, ITableDrawingService drawingService) :base("Account") 
        {   
            //_pagedMenu = pagedMenu;
            _loginService = loginService;
            _accountService = accountService;
            AddExitOption("Back");
            AddItem("Log In", () => LogIn());
            AddItem("Log Out", () => LogOut());
            AddItem("Create Account", () => CreateAccount());
            AddItem("Update Account", () => UpdateAccount());
            AddItem("Delete Account", () => DeleteAccount());
            AddItem("My account", () => ShowMyAccInfo());
            AddItem("Find by login", () => ShowByLogin());
            AddSubMenu("All accounts", new PagedMenu<IAccountService, UserDtoResponse, UserFilter>(sp, drawingService, "Accounts"));
        }

        public async Task LogIn(CancellationToken ct = default)
        {
            var email = await AnsiConsole.AskAsync<string>("[yellow]Enter email[/]");
            var password = await AnsiConsole.AskAsync<string>("[yellow]Enter password[/]");
            var loginRequest = new LoginRequest(email, password);
            await _loginService.LogIn(loginRequest, ct);
        }
        public async Task LogOut(CancellationToken ct = default)
        {
            await _loginService.LogOut(ct);
        }

        public async Task CreateAccount(CancellationToken ct=default)
        {
            var user = new UserDtoCreation
            {
                FirstName = "Arseni",
                LastName = "Hapechkin",
                Email = "Solomonlol95@gmail.com",
                Login = "Solomon",
                PhoneNumber = "1234567890",
                Password = "Password123",
                RepeatPassword = "Password123"

            };
            await _accountService.Create(user, ct);
        }

        public async Task UpdateAccount(CancellationToken ct=default)
        {
            var user = new UserDtoRequest
            {
                FirstName = "Anton",
                LastName = "Gorodetsky",
                Email = "Sumrak@gmail.com",
                Login = "Sumrak",
                PhoneNumber = "321 12 3216598",
                Role = "Admin"
            };
            await _accountService.Update(user, ct);
        }

        public async Task DeleteAccount(CancellationToken ct=default)
        {
            var login = await AnsiConsole.AskAsync<string>("[yellow]Enter login to delete.[/]");
            await _accountService.Delete(login, ct);
        }

        public async Task ShowByLogin(CancellationToken ct=default)
        {
            var login = await AnsiConsole.AskAsync<string>("[yellow]Enter login of account:[/]");
            await _accountService.GetOne(login, ct);
        }

        public async Task ShowMyAccInfo(CancellationToken ct=default)
        {
            await _accountService.GetMe(ct);
        }
    }
}
