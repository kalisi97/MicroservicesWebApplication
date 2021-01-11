using ShoppingBasket.Entities;
using ShoppingBasket.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShoppingBasket.Services
{
    public class WineCatalogService : IWineCatalogService
    {
        private readonly HttpClient client;

        public WineCatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Wine> GetWine(Guid id)
        {
            var response = await client.GetAsync($"/api/wines/{id}");
            return await response.ReadContentAs<Wine>();
        }
    }
}
