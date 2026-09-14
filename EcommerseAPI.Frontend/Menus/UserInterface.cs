using EcommerseAPI.Frontend.Entities;
using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;

namespace EcommerseAPI.Frontend.Menus
{
    internal class UserInterface : IMenu
    {
        private readonly string _name;
        private readonly List<MenuItem> _menus = new();
        private bool _exit = false;

        public UserInterface(string name) => _name = name;

        public void AddItem(string name, Func<Task> action) =>
            _menus.Add(new MenuItem(name, action));

        public void AddSubMenu(string name, IMenu menu) =>
            _menus.Add(new MenuItem(name, menu));

        public void AddExitOption(string name = "Back") =>
            AddItem(name, () => { _exit = true; return Task.CompletedTask; });
        public async Task StartAsync(CancellationToken ct = default)
        {
            Console.Clear();
            _exit = false;

            await OnStartingAsync(ct);
            while (!_exit)
            {
                var choises = _menus.Select(m => m.Name).ToList();
                var choise = await AnsiConsole.PromptAsync(
                    new SelectionPrompt<string>()
                    .Title($"[green]{_name}[/]")
                    .AddChoices(choises));

                var selected = _menus.First(m => m.Name == choise);
                if (selected.Action != null)
                    await selected.Action();
                else if (selected.SubMenu != null)
                    await selected.SubMenu.StartAsync(ct);
            }
        }
        protected virtual Task OnStartingAsync(CancellationToken ct = default) =>
            Task.CompletedTask;
    }
}
