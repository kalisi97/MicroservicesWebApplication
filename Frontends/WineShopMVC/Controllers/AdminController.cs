using Microsoft.AspNetCore.Mvc;
using Models.Api;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineShopMVC.Models.Api;

namespace WineShopMVC.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly IWineCatalogService wineCatalogService;

        public AdminController(IWineCatalogService wineCatalogService /*Settings settings*/)
        {
            this.wineCatalogService = wineCatalogService;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await wineCatalogService.GetAll();
            return View(allProducts);
        }

        public async Task<IActionResult> Details(Guid wineId)
        {
            var selectedProduct = await wineCatalogService.GetWine(wineId);
            var selectedProduct2 = (await wineCatalogService.GetAll())
                .Where(w => w.WineId == wineId).FirstOrDefault();
            return View(selectedProduct);
        }

        [HttpPost]

        public async Task<IActionResult> Details(Wine wineUpdatedPriceViewModel)
        {
            PriceChange priceChange = new PriceChange
            {
                Price = wineUpdatedPriceViewModel.Price,
                WineId = wineUpdatedPriceViewModel.WineId
            };

            await wineCatalogService.UpdatePrice(priceChange);

            return RedirectToAction("Index");
        }
    }
}
