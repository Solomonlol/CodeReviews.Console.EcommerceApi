using EcommerseAPI.Frontend.Interfaces;
using EcommerseAPI.Frontend.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerseAPI.Frontend.Services.Factory
{
    internal class UrlServiceFactory : IUrlServiceFactory
    {
        public UrlService Create(string baseUrl)
        {
            return new UrlService(baseUrl);
        }
    }
}
