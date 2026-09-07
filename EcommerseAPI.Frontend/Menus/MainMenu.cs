using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Menus
{
    internal class MainMenu : UserInterface
    {
        public MainMenu(string title) :base(title)
        {
            AddItem("LogIn", ()=> LogIn());
            AddExitItem("Exit");
        }

        public async Task LogIn(CancellationToken ct = default)
        {

        }
    }
}
