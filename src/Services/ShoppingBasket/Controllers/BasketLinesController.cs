using AutoMapper;
using ShoppingBasket.Entities;
using ShoppingBasket.Models;
using ShoppingBasket.Repositories;
using ShoppingBasket.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasketLine = ShoppingBasket.Models.BasketLine;

namespace ShoppingBasket.Controllers
{
    [Route("api/baskets/{basketId}/basketlines")]
    [ApiController]
    public class BasketLinesController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly IBasketLinesRepository basketLinesRepository;
        private readonly IWineRepository WineRepository;
        private readonly IWineCatalogService WineCatalogService;
        private readonly IMapper mapper;
        private readonly IBasketChangeRepository basketChangeRepository;

        public BasketLinesController(IBasketRepository basketRepository,
            IBasketLinesRepository basketLinesRepository, IWineRepository WineRepository,
            IWineCatalogService WineCatalogService, IMapper mapper, IBasketChangeRepository basketChangeRepository)
        {
            this.basketRepository = basketRepository;
            this.basketLinesRepository = basketLinesRepository;
            this.WineRepository = WineRepository;
            this.WineCatalogService = WineCatalogService;
            this.basketChangeRepository = basketChangeRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketLine>>> Get(Guid basketId)
        {
            if (!await basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            var basketLines = await basketLinesRepository.GetBasketLines(basketId);
            return Ok(mapper.Map<IEnumerable<BasketLine>>(basketLines));
        }

        [HttpGet("{basketLineId}", Name = "GetBasketLine")]
        public async Task<ActionResult<BasketLine>> Get(Guid basketId,
            Guid basketLineId)
        {
            if (!await basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            var basketLine = await basketLinesRepository.GetBasketLineById(basketLineId);
            if (basketLine == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BasketLine>(basketLine));
        }

        [HttpPost]
        public async Task<ActionResult<BasketLine>> Post(Guid basketId, [FromBody] BasketLineForCreation basketLineForCreation)
        {
            var basket = await basketRepository.GetBasketById(basketId);

            if (basket == null)
            {
                return NotFound();
            }
            // pokreni i catalog servis
           // var WineFromCatalog = await WineCatalogService.GetWine(basketLineForCreation.WineId);

            Entities.Wine wine = await WineRepository.WineExists(basketLineForCreation.WineId);

            if (wine == null)
            {
               // WineRepository.AddWine(WineFromCatalog);
                await WineRepository.SaveChanges();
            }

            var basketLineEntity = mapper.Map<Entities.BasketLine>(basketLineForCreation);

            var processedBasketLine = await basketLinesRepository.AddOrUpdateBasketLine(basketId, basketLineEntity);
            await basketLinesRepository.SaveChanges();

            var mappedWine = mapper.Map<Models.Wine>(wine);
            var basketLineToReturn = mapper.Map<BasketLine>(processedBasketLine);
            basketLineToReturn.Wine = mappedWine;

            var basketChange = new BasketChange
            {
                BasketChangeType = BasketChangeTypeEnum.Add,
                WineId = basketLineForCreation.WineId,
                InsertedAt = DateTime.Now,
                UserId = basket.UserId
            };
            await basketChangeRepository.AddBasketChange(basketChange);

            return CreatedAtRoute(
                "GetBasketLine",
                new { basketId = basketLineEntity.BasketId, basketLineId = basketLineEntity.BasketLineId },
                basketLineToReturn);
        }

        [HttpPut("{basketLineId}")]
        public async Task<ActionResult<BasketLine>> Put(Guid basketId,
            Guid basketLineId,
            [FromBody] BasketLineForUpdate basketLineForUpdate)
        {
            if (!await basketRepository.BasketExists(basketId))
            {
                return NotFound();
            }

            var basketLineEntity = await basketLinesRepository.GetBasketLineById(basketLineId);

            if (basketLineEntity == null)
            {
                return NotFound();
            }

            // map the entity to a dto
            // apply the updated field values to that dto
            // map the dto back to an entity
            mapper.Map(basketLineForUpdate, basketLineEntity);

            basketLinesRepository.UpdateBasketLine(basketLineEntity);
            await basketLinesRepository.SaveChanges();

            return Ok(mapper.Map<BasketLine>(basketLineEntity));
        }

        [HttpDelete("{basketLineId}")]
        public async Task<IActionResult> Delete(Guid basketId, Guid basketLineId)
        {
            //if (!await basketRepository.BasketExists(basketId))
            //{
            //    return NotFound();
            //}

            var basket = await basketRepository.GetBasketById(basketId);

            if (basket == null)
            {
                return NotFound();
            }

            var basketLineEntity = await basketLinesRepository.GetBasketLineById(basketLineId);

            if (basketLineEntity == null)
            {
                return NotFound();
            }

            basketLinesRepository.RemoveBasketLine(basketLineEntity);
            await basketLinesRepository.SaveChanges();

            //publish removal Wine
            BasketChange basketChangeWine = new BasketChange
            {
                BasketChangeType = BasketChangeTypeEnum.Remove,
                WineId = basketLineEntity.WineId,
                InsertedAt = DateTime.Now,
                UserId = basket.UserId
            };
            await basketChangeRepository.AddBasketChange(basketChangeWine);

            return NoContent();
        }
    }
}
