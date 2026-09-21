namespace EcommerseAPI.Frontend.Menus
{
    internal class MainMenu : UserInterface
    {
        private readonly CatalogMenu _catalogMenu;
        private readonly AccountMenu _accountMenu;
        public MainMenu(CatalogMenu productMenu, AccountMenu accountMenu) : base("Main menu")
        {
            _accountMenu = accountMenu;
            _catalogMenu = productMenu;
            AddSubMenu("Catalog", _catalogMenu);
            AddSubMenu("Account", _accountMenu);
            AddExitOption("Exit");
        }
    }
}
