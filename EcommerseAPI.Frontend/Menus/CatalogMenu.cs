using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Menus
{
    internal class CatalogMenu : UserInterface
    {
        public CatalogMenu() : base("Catalog")
        {
            AddExitOption("Back");
            AddItem("All products list", () => ShowAllProducts());
            AddItem("Choose category", () => ChooseCategory());
        }


        public async Task ShowAllProducts(CancellationToken ct = default)
        {

        }

        public async Task ChooseCategory(CancellationToken ct = default)
        {

        }
    }
}
