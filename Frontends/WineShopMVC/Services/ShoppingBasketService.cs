﻿using Extensions;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Models;
using Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WineShopMVC.Models;

namespace Services
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ShoppingBasketService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<BasketLine> AddToBasket(Guid basketId, BasketLineForCreation basketLine)
        {
            if (basketId == Guid.Empty)
            {
                client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
                var basketResponse = await client.PostAsJson("api/baskets", new BasketForCreation { UserId = 
                        Guid.Parse(httpContextAccessor.HttpContext
                        .User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value)
                });
                var basket = await basketResponse.ReadContentAs<Basket>();
                basketId = basket.BasketId;
            }
            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.PostAsJson($"api/baskets/{basketId}/basketlines", basketLine);
            return await response.ReadContentAs<BasketLine>();
        }

        public async Task<Basket> GetBasket(Guid basketId)
        {
            if (basketId == Guid.Empty)
                return null;
            var token = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
           
            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync($"api/baskets/{basketId}");
            return await response.ReadContentAs<Basket>();
        }

        public async Task<IEnumerable<BasketLine>> GetLinesForBasket(Guid basketId)
        {
            if (basketId == Guid.Empty)
                return new BasketLine[0];
            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync($"api/baskets/{basketId}/basketLines");
            return await response.ReadContentAs<BasketLine[]>();

        }

        public async Task UpdateLine(Guid basketId, BasketLineForUpdate basketLineForUpdate)
        {
            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            await client.PutAsJson($"api/baskets/{basketId}/basketLines/{basketLineForUpdate.LineId}", basketLineForUpdate);
        }

        public async Task RemoveLine(Guid basketId, Guid lineId)
        {
            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            await client.DeleteAsync($"api/baskets/{basketId}/basketLines/{lineId}");
        }

        public async Task<BasketForCheckout> Checkout(Guid basketId, BasketForCheckout basketForCheckout)
        {
            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.PostAsJson($"api/baskets/checkout", basketForCheckout);
            if(response.IsSuccessStatusCode)
                return await response.ReadContentAs<BasketForCheckout>();
            else
            {
                throw new Exception("Something went wrong placing your order. Please try again.");
            }
        }

        public async Task<Basket> GetBasketForUser(Guid userId)
        {
            if (userId == Guid.Empty)
                return null;
            var token = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync($"api/baskets/user/{userId}");
            return await response.ReadContentAs<Basket>();
        }

        public  async Task<Basket> CreateBasketForUser(Guid userId)
        {
            if (userId == Guid.Empty)
                return null;

            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.PostAsJson($"api/baskets", new BasketForCreation { UserId = userId});
            return await response.ReadContentAs<Basket>();
        }

        public async Task<Coupon> GetDiscountForBasket(Guid userId)
        {
            if (userId == Guid.Empty)
                return null;

            client.SetBearerToken(await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync($"api/baskets/discount/{userId}");
            return await response.ReadContentAs<Coupon>();
        }
    }
}
