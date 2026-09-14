using EcommerseAPI.Frontend.Interfaces;

namespace EcommerseAPI.Frontend.Entities
{
    internal class MenuItem
    {
        public string Name { get; }
        public Func<Task?> Action { get; }
        public IMenu SubMenu { get; }

        public MenuItem(string name, Func<Task> action) => (Name, Action) = (name, action);
        public MenuItem(string name, IMenu menu) => (Name, SubMenu) = (name, menu);
    }
}
