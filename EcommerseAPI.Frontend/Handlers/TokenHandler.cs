using EcommerseAPI.Frontend.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace EcommerseAPI.Frontend.Handlers
{
    internal class TokenHandler : DelegatingHandler
    {
        private readonly ITokenService _tokenService;
        public TokenHandler(ITokenService tokenService) 
        {
            _tokenService = tokenService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
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
