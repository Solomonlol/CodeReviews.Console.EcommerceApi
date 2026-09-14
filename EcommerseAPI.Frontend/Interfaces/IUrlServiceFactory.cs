using EcommerseAPI.Frontend.Services;

namespace EcommerseAPI.Frontend.Interfaces
{
    internal interface IUrlServiceFactory
    {
        UrlService Create(string baseUrl);
    }
}
