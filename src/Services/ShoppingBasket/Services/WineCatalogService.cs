using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using ShoppingBasket.Entities;
using ShoppingBasket.Extensions;
using ShoppingBasket.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace ShoppingBasket.Services
{
    public class WineCatalogService : IWineCatalogService
    {
        private readonly HttpClient client;
        private string _accessToken;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TokenExchangeService tokenExchangeService;
        public WineCatalogService(HttpClient client, IHttpContextAccessor httpContextAccessor, TokenExchangeService tokenExchangeService)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
            this.tokenExchangeService = tokenExchangeService;
        }
        //this or we can use TokenExchangeService
        private async Task<string> GetToken()
        {
            if (!string.IsNullOrWhiteSpace(_accessToken))
            {
                return _accessToken;
            }

            var discoveryDocumentResponse = await client
                .GetDiscoveryDocumentAsync("https://localhost:5010/");
            if (discoveryDocumentResponse.IsError)
            {
                throw new Exception(discoveryDocumentResponse.Error);
            }

            var customParams = new Dictionary<string, string>
            {
                { "subject_token_type", "urn:ietf:params:oauth:token-type:access_token"},
                { "subject_token", await httpContextAccessor.HttpContext.GetTokenAsync("access_token")},
                { "scope", "openid profile winecatalog.fullaccess" }
            };

            var tokenResponse = await client.RequestTokenAsync(new TokenRequest()
            {
                Address = discoveryDocumentResponse.TokenEndpoint,
                GrantType = "urn:ietf:params:oauth:grant-type:token-exchange",
                Parameters = customParams,
                ClientId = "shoppingbaskettodownstreamtokenexchangeclient",
                ClientSecret = "0cdea0bc-779e-4368-b46b-09956f70712c"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            _accessToken = tokenResponse.AccessToken;
            return _accessToken;
        }

        public async Task<Wine> GetWine(Guid id)
        {
            var incomingToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var accessTokenForWineCatalogService = await tokenExchangeService
           .GetTokenAsync(incomingToken, "winecatalog.fullaccess");
            client.SetBearerToken(accessTokenForWineCatalogService);
            var response = await client.GetAsync($"/api/wines/{id}");
            return await response.ReadContentAs<Wine>();
        }
    }
}
