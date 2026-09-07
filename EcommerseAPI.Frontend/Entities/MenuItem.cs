using EcommerseAPI.Frontend.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Entities
{
    internal class MenuItem
    {
        public IMenu? SubMenu {  get; }
        public Func<Task?> Action { get; }
        public string Name { get; }

        public MenuItem(string name, Func<Task> action) => (Name, Action) = (name, action);
        public MenuItem(string name, IMenu menu) => (Name, SubMenu) = (name, menu);

    }
}
