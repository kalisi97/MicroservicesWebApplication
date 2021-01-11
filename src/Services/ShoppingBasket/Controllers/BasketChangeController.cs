using AutoMapper;
using ShoppingBasket.Models;
using ShoppingBasket.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingBasket.Controllers
{
    [ApiController]
    [Route("api/basketChange")]
    public class BasketChangeController : Controller
    {
        private readonly IBasketChangeRepository basketChangeRepository;
        private readonly IMapper mapper;

        public BasketChangeController(IMapper mapper, IBasketChangeRepository basketChangeRepository)
        {
            this.mapper = mapper;
            this.basketChangeRepository = basketChangeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetWines([FromQuery] DateTime fromDate, [FromQuery] int max)
        {
            var Wines = await basketChangeRepository.GetBasketChanges(fromDate, max);
            return Ok(mapper.Map<List<BasketChangeForPublication>>(Wines));
        }
    }
}
