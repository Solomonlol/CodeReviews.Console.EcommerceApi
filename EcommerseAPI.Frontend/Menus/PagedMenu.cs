using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Menus
{
    internal class PagedMenu : UserInterface
    {
        private string _url;
        public PagedMenu() : base("Paged result") 
        {
            AddExitOption();
        }

        public async Task NextPage(string url, int currentPage, int lastPage, CancellationToken ct=default)
        {

        }

        
    }
}
