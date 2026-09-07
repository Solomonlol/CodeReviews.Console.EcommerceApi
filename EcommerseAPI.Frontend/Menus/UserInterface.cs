using EcommerseAPI.Frontend.Entities;
using EcommerseAPI.Frontend.Interfaces;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Menus
{
    internal class UserInterface : IMenu
    {
        private readonly string _title;
        private readonly List<MenuItem> _menus=new();
        private bool _exit;

        public UserInterface(string title) => _title = title;
        public void AddItem(string name, Func<Task> action) =>
            _menus.Add(new MenuItem(name, action));

        public void AddSubMenu(string name, IMenu subMenu) =>
            _menus.Add(new MenuItem(name, subMenu));

        public void AddExitItem(string name = "Back") =>
            AddItem(name, () => { _exit = true; return Task.CompletedTask; });
        public  async Task StartAsync(CancellationToken ct = default)
        {
            Console.Clear();
            while(!_exit)
            {
                var choises = _menus.Select(m=>m.Name).ToList();
                var choise = await AnsiConsole.PromptAsync(
                    new SelectionPrompt<string>()
                    .Title($"[green]{_title}[/]")
                    .AddChoices(choises), ct);

                var selected = _menus.First(m => m.Name == choise);
                if (selected.Action != null)
                    await selected.Action();
                else if (selected.SubMenu != null)
                    await selected.SubMenu.StartAsync(ct);
                
            }
        }
    }
}
