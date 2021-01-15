using Extensions;
using Models.Api;
using Models.View;
using Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WineShopMVC.Models;

namespace Controllers
{
    public class ShoppingBasketController : Controller
    {
        private readonly IShoppingBasketService basketService;
   
        private readonly Settings settings;

        public ShoppingBasketController(IShoppingBasketService basketService,
            Settings settings)
        {
            this.basketService = basketService;
            this.settings = settings;
          
        }

        public async Task<IActionResult> Index()
        {
            var basketViewModel = await CreateBasketViewModel();

            return View(basketViewModel);
        }

        private async Task<BasketViewModel> CreateBasketViewModel()
        {
            var basketId = settings.BasketId;
            Basket basket = await basketService.GetBasket(basketId);
            Coupon coupon = await basketService.GetDiscountForBasket(basket.UserId);
            var basketLines = await basketService.GetLinesForBasket(basketId);

            var lineViewModels = basketLines.Select(bl => new BasketLineViewModel
            {
                LineId = bl.BasketLineId,
                WineId = bl.WineId,
                Name = bl.Wine.Name,
                Price = bl.Price,
                Quantity = bl.Quantity
            });


            var basketViewModel = new BasketViewModel
            {
                BasketLines = lineViewModels.ToList()
                
            };

            if (coupon != null && coupon.AlreadyUsed == false) basketViewModel.Discount = coupon.Amount;
            else basketViewModel.Discount = 0;
             
            basketViewModel.ShoppingCartTotal = basketViewModel.BasketLines.Sum(bl => bl.Price * bl.Quantity);

            return basketViewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLine(BasketLineForCreation basketLine)
        {
            var basketId = settings.BasketId;
            var newLine = await basketService.AddToBasket(basketId, basketLine);
           // Response.Cookies.Append(settings.BasketIdCookieName, newLine.BasketId.ToString());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLine(BasketLineForUpdate basketLineUpdate)
        {
            var basketId = settings.BasketId;
            await basketService.UpdateLine(basketId, basketLineUpdate);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveLine(Guid lineId)
        {
            var basketId = settings.BasketId;
            await basketService.RemoveLine(basketId, lineId);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(BasketCheckoutViewModel basketCheckoutViewModel)
        {
            try
            {
                var basketId = settings.BasketId;
                if (ModelState.IsValid)
                {
                    var basketForCheckout = new BasketForCheckout
                    {
                        FirstName = basketCheckoutViewModel.FirstName,
                        LastName = basketCheckoutViewModel.LastName,
                        Email = basketCheckoutViewModel.Email,
                        Address = basketCheckoutViewModel.Address,
                        ZipCode = basketCheckoutViewModel.ZipCode,
                        City = basketCheckoutViewModel.City,
                        Country = basketCheckoutViewModel.Country,
                        CardNumber = basketCheckoutViewModel.CardNumber,
                        CardName = basketCheckoutViewModel.CardName,
                        CardExpiration = basketCheckoutViewModel.CardExpiration,
                        CvvCode = basketCheckoutViewModel.CvvCode,
                        BasketId = basketId,
                        UserId =   Guid.Parse(User.Claims
                        .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value)
                    };

                    await basketService.Checkout(basketCheckoutViewModel.BasketId, basketForCheckout);

                    return RedirectToAction("CheckoutComplete");
                }

                return View(basketCheckoutViewModel);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View(basketCheckoutViewModel);
            }
        } 

        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}
