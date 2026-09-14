using EcommerseAPI.Frontend.Interfaces;
using System.Net.Http.Headers;

namespace EcommerseAPI.Frontend.Handlers
{
    internal class TokenHandler : DelegatingHandler
    {
        private readonly ITokenService _tokenService;
        public TokenHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct = default)
        {
            var token = await _tokenService.GetToken(ct);
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, ct);
        }
    }
}
