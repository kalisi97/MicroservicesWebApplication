// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity
{
    //contains only barebones configuration, only the essential parts
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(), // subjectid and userid will be returned when the openid is requested
                new IdentityResources.Profile(), // when the profile id is requested, some additional data can be returned like familiy name
            };

        //each microservice represents an api resource
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("winecatalog", "Wine catalog API")
                {
                    Scopes = { "winecatalog.fullaccess"}
                },
                new ApiResource("shoppingbasket", "Shopping basket API")
                {
                    Scopes = { "shoppingbasket.fullaccess" }
                },
                new ApiResource("discount", "Discount API")
                {
                    Scopes = { "discount.fullaccess" }
                },
                  new ApiResource("wineshopgateway", "WineShop Gateway")
                {
                    Scopes = { "wineshopgateway.fullaccess" }
                },
                  new ApiResource("ordering", "Ordering API")
                  {
                      Scopes = {"ordering.fullaccess"}
                  }
            };


        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
              //nesto sto daje pristup necemu unutar mikroservisa, npr read scope, write scope, ova vrednost ce biti u audience claim-u
                new ApiScope("winecatalog.fullaccess"),
                new ApiScope("shoppingbasket.fullaccess"),
                new ApiScope("discount.fullaccess"),
                new ApiScope("wineshopgateway.fullaccess"),
                new ApiScope("ordering.fullaccess")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // in order to allow communication between shopping basket and discount service
                  new Client
                {
                    ClientId = "shoppingbaskettodownstreamtokenexchangeclient",
                    ClientName = "Shopping Basket Token Exchange Client",
                    AllowedGrantTypes = new[] { "urn:ietf:params:oauth:grant-type:token-exchange" },
                    ClientSecrets = { new Secret("0cdea0bc-779e-4368-b46b-09956f70712c".Sha256()) },
                    AllowedScopes = {
                         "openid", "profile", "discount.fullaccess", "ordering.fullaccess" }
                },
                  /*
                new Client
                {
                    ClientName = "WineShop Machine 2 Machine Client",
                    ClientId = "wineshopm2m",
                    ClientSecrets = { new Secret("eac7008f-1b35-4325-ac8d-4a71932e6088".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "winecatalog.fullaccess" }
                },
                new Client
                {
                    ClientName = "WineShop Interactive Client",
                    ClientId = "wineshopinteractive",
                    ClientSecrets = { new Secret("ce766e16-df99-411d-8d31-0f5bbc6b8eba".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5000/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5000/signout-callback-oidc" },
                    RequireConsent = false,
                    AllowedScopes = { "openid", "profile", "shoppingbasket.fullaccess" }
                },
                */
                 new Client
                {
                    ClientName = "WineShop Client",
                    ClientId = "wineshop",
                    ClientSecrets = { new Secret("ce766e16-df99-411d-8d31-0f5bbc6b8eba".Sha256()) },
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RedirectUris = { "https://localhost:5000/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5000/signout-callback-oidc" },
                    RequireConsent = false,
                    AllowedScopes = { "openid", "profile", "wineshopgateway.fullaccess", "shoppingbasket.fullaccess", "winecatalog.fullaccess" }
                },

            };
    }
}