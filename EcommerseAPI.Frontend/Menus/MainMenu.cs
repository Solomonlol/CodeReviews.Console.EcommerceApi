using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Menus
{
    internal class MainMenu : UserInterface
    {
        private readonly ProductMenu _productMenu;
        public MainMenu(ProductMenu productMenu) : base("Main menu")
        {
            _productMenu = productMenu;
            AddSubMenu("Products", _productMenu);
            AddItem("Login", () => Login());
            AddExitOption("Exit");
        }

        public async Task Login(CancellationToken ct=default)
        {

        }

        
    }
}
