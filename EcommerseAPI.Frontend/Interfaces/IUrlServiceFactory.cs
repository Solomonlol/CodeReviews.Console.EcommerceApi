using EcommerseAPI.Frontend.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IUrlServiceFactory
    {
        UrlService Create(string baseUrl);
    }
}
