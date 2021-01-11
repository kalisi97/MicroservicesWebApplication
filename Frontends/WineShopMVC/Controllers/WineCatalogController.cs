using System;
using System.Threading.Tasks;
using Extensions;
using Models;
using Models.Api;
using Models.View;
using Services;
using Microsoft.AspNetCore.Mvc;
using WineShopMVC.Models;
using System.Linq;
using System.Security.Claims;

namespace Controllers
{
    public class WineCatalogController : Controller
    {
        private readonly IWineCatalogService wineCatalogService;
        private readonly IShoppingBasketService shoppingBasketService;
        private readonly Settings settings;

        public WineCatalogController(IWineCatalogService wineCatalogService, 
            IShoppingBasketService shoppingBasketService, 
            Settings settings)
        {
            this.wineCatalogService = wineCatalogService;
            this.shoppingBasketService = shoppingBasketService;
            this.settings = settings;
        }

        public async Task<IActionResult> Index(Guid categoryId)
        {
            try
            {
;                //     var currentBasketId = Request.Cookies.GetCurrentBasketId(settings);

                Guid userId = Guid.Parse(HttpContext.User.Claims.ToList().ElementAt(2).Value);

  
                var getBasket =  shoppingBasketService.GetBasketForUser(userId);

                // if basket is null, create new basket

                settings.BasketId = getBasket.Result.BasketId;

                //       var getBasket = currentBasketId == Guid.Empty ? Task.FromResult<Basket>(null) :
                //          shoppingBasketService.GetBasket(currentBasketId);

                var getCategories = wineCatalogService.GetCategories();

                var getWines = categoryId == Guid.Empty ? wineCatalogService.GetAll() :
                    wineCatalogService.GetByCategoryId(categoryId);


                await Task.WhenAll(new Task[] { //getBasket,
                    getCategories, getWines });

               var numberOfItems = getBasket.Result?.NumberOfItems ?? 0;

                return View(
                    new WineListModel
                    {
                        Wines = getWines.Result,
                        Categories = getCategories.Result,
                        NumberOfItems = numberOfItems,
                        SelectedCategory = categoryId
                    }
                );
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult SelectCategory([FromForm] Guid selectedCategory)
        {
            return RedirectToAction("Index", new { categoryId = selectedCategory });
        }

        public async Task<IActionResult> Detail(Guid wineId)
        {
            var ev = await wineCatalogService.GetWine(wineId);
            return View(ev);
        }
    }
}
