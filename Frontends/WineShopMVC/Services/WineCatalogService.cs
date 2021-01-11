using Extensions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Models.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WineShopMVC.Models.Api;

namespace Services
{
    public class WineCatalogService : IWineCatalogService
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;
        public WineCatalogService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }



        public async Task<IEnumerable<Wine>> GetAll()
        {

            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync("api/wines");
            return await response.ReadContentAs<List<Wine>>();
        }

        public async Task<IEnumerable<Wine>> GetByCategoryId(Guid categoryid)
        {
           client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync($"api/wines/?categoryId={categoryid}");
            return await response.ReadContentAs<List<Wine>>();
        }

        public async Task<Wine> GetWine(Guid id)
        {
            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync($"api/wines/{id}");
            return await response.ReadContentAs<Wine>();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync("api/categories");
            return await response.ReadContentAs<List<Category>>();
        }

        public async Task<PriceChange> UpdatePrice(PriceChange priceChange)
        {
            // todo just for admins!!!!!
            var token = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            var user = httpContextAccessor.HttpContext.User;
            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var reponse = await client.PostAsJson($"api/wines/winepriceupdate", priceChange);
            return await reponse.ReadContentAs<PriceChange>();
        }
    }
}
