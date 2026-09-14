using EcommerseAPI.Frontend.Interfaces;

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
