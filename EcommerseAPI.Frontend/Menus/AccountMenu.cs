using EcommerseAPI.Frontend.Entities;
using EcommerseAPI.Frontend.Entities.Dto.Users;
using EcommerseAPI.Frontend.Entities.Filters;
using EcommerseAPI.Frontend.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace EcommerseAPI.Frontend.Menus
{
    internal class AccountMenu : UserInterface
    {
        private readonly IAccountService _accountService;
        private readonly ILoginService _loginService;

        public AccountMenu(IServiceProvider sp) : base("Account")
        {

            _loginService = sp.GetRequiredService<ILoginService>();
            _accountService = sp.GetRequiredService<IAccountService>();
            AddExitOption("Back");
            AddItem("Log In", () => LogIn());
            AddItem("Log Out", () => LogOut());
            AddItem("Create Account", () => CreateAccount());
            AddItem("Update Account", () => UpdateAccount());
            AddItem("Delete Account", () => DeleteAccount());
            AddItem("My account", () => ShowMyAccInfo());
            AddItem("Find by login", () => ShowByLogin());
            AddSubMenu("All accounts", new PagedMenu<IAccountService, UserDtoResponse, UserFilter>(sp, "Accounts"));
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

        public async Task CreateAccount(CancellationToken ct = default)
        {
            var user = new UserDtoCreation
            {
                FirstName = await AnsiConsole.AskAsync<string>($"[yellow]Enter first name:[/]"),
                LastName = await AnsiConsole.AskAsync<string>($"[yellow]Enter last name:[/]"),
                Email = await AnsiConsole.AskAsync<string>($"[yellow]Enter email:[/]"),
                Login = await AnsiConsole.AskAsync<string>($"[yellow]Enter login:[/]"),
                PhoneNumber = await AnsiConsole.AskAsync<string>($"[yellow]Enter phone number:[/]"),
                Password = await AnsiConsole.AskAsync<string>($"[yellow]Enter password:[/]"),
                RepeatPassword = await AnsiConsole.AskAsync<string>($"[yellow]Repeat password:[/]")
            };
            if (await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                await _accountService.Create(user, ct);
            else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
        }

        public async Task UpdateAccount(CancellationToken ct = default)
        {
            await _accountService.Update(ct);
        }

        public async Task DeleteAccount(CancellationToken ct = default)
        {
            var login = await AnsiConsole.AskAsync<string>("[yellow]Enter login to delete.[/]");

            if (await AnsiConsole.ConfirmAsync("Are you sure?", cancellationToken: ct))
                await _accountService.Delete(login, ct);
            else AnsiConsole.MarkupLine("[violet]The operation was cancelled.[/]");
        }

        public async Task ShowByLogin(CancellationToken ct = default)
        {
            var login = await AnsiConsole.AskAsync<string>("[yellow]Enter login of account:[/]");
            await _accountService.GetOne(login, ct);
        }

        public async Task ShowMyAccInfo(CancellationToken ct = default)
        {
            await _accountService.GetMe(ct);
        }
    }
}
