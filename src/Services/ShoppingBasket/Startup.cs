using AutoMapper;
using MessagingBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShoppingBasket.DbContexts;
using ShoppingBasket.Helpers;
using ShoppingBasket.Repositories;
using ShoppingBasket.Services;
using ShoppingBasket.Services.Worker;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddAccessTokenManagement();
            services.AddScoped<TokenExchangeService>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.Authority = "https://localhost:5010";
               options.Audience = "shoppingbasket";
           });

            services.AddHostedService<ServiceBusListener>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var requireAuthenticateUserPolicy = new AuthorizationPolicyBuilder()
              .RequireAuthenticatedUser().Build();

            services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter(requireAuthenticateUserPolicy));
            });

            //ServiceBusListener je singleton i zato ne moze da pristupi BasketLinesRepo koji je scoped, isto vazi i za DbContext
            var optionsBuilder = new DbContextOptionsBuilder<ShoppingBasketDbContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            services.AddSingleton(new BasketLinesIntegrationRepository(optionsBuilder.Options));

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketLinesRepository, BasketLinesRepository>();
            services.AddScoped<IWineRepository, WineRepository>();
            services.AddScoped<IBasketChangeRepository, BasketChangeRepository>();

            services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();
            // in order to avoid socket exhaustion expection
            services.AddHttpClient<IWineCatalogService, WineCatalogService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiConfigs:WineCatalog:Uri"]));

            services.AddHttpClient<IDiscountService, DiscountService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiConfigs:Discounts:Uri"]));


            services.AddDbContext<ShoppingBasketDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping Basket API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping Basket API V1");

            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
