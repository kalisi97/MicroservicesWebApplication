using Models;
using Models.View;
using Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WineShopMVC.Models;
using System;
using System.Linq;

namespace Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly Settings settings;

        public OrderController(Settings settings, IOrderService orderService)
        {
            this.settings = settings;
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await orderService.GetOrdersForUser(
                Guid.Parse(
                    User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value));

            return View(new OrderViewModel { Orders = orders });
        }
    }
}
