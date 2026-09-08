using EcommerseAPI.Frontend.Entities;
using EcommerseAPI.Frontend.Interfaces;
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

        public AccountMenu(IAccountService accountService, ILoginService loginService) :base("Account") 
        {
            _loginService = loginService;
            _accountService = accountService;
            AddExitOption("Back");
            AddItem("Log In", () => LogIn());
            AddItem("Log Out", () => LogOut());
            AddItem("Create Account", () => CreateAccount());
            AddItem("Delete Account", () => DeleteAccount());
            AddItem("My account", () => ShowAccInfo());
            AddItem("All accounts", () => ShowAll());
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

        public async Task DeleteAccount(CancellationToken ct=default)
        {
            var login = await AnsiConsole.AskAsync<string>("[yellow]Enter login to delete.[/]");
            await _accountService.Delete(login, ct);
        }

        public async Task ShowAll(CancellationToken ct=default)
        {
            await _accountService.GetAll(ct);
        }

        public async Task ShowAccInfo(CancellationToken ct=default)
        {

        }
    }
}
