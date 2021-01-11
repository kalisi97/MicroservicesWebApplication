using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.View;
using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WineShopMVC.Models;
using WineShopMVC.Models.View;

namespace WineShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWineCatalogService wineCatalogService;
        private readonly IShoppingBasketService shoppingBasketService;
        private readonly Settings settings;

        public HomeController(IWineCatalogService wineCatalogService,
            IShoppingBasketService shoppingBasketService,
            Settings settings)
        {
            this.wineCatalogService = wineCatalogService;
            this.shoppingBasketService = shoppingBasketService;
            this.settings = settings;
        }

        public async Task<IActionResult> Index(Guid categoryId)
        {
            var getCategories = wineCatalogService.GetCategories();

            var getWines = categoryId == Guid.Empty ? wineCatalogService.GetAll() :
                wineCatalogService.GetByCategoryId(categoryId);


            await Task.WhenAll(new Task[] { getCategories, getWines });

        

            return View(
                new WineCatalogListModel
                {
                    Wines = getWines.Result,
                    Categories = getCategories.Result,
                    SelectedCategory = categoryId
                }
            );
         
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
