using AutoMapper;
using MessagingBus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineCatalog.Messages;
using WineCatalog.Models;
using WineCatalog.Repositories;

namespace WineCatalog.Controllers
{
    [Route("api/wines")]
    [ApiController]
    public class WineController : ControllerBase
    {
        private readonly IWineRepository WineRepository;
        private readonly IMapper mapper;
        private readonly IMessageBus messageBus;

        public WineController(IWineRepository WineRepository, IMapper mapper, IMessageBus messageBus)
        {
            this.WineRepository = WineRepository;
            this.mapper = mapper;
            this.messageBus = messageBus;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.WineDTO>>> Get(
            [FromQuery] Guid categoryId)
        {
            var result = await WineRepository.GetWines(categoryId);
            return Ok(mapper.Map<List<Models.WineDTO>>(result));
        }

        [HttpGet("{WineId}")]
        public async Task<ActionResult<Models.WineDTO>> GetById(Guid WineId)
        {
            var result = await WineRepository.GetWineById(WineId);
            return Ok(mapper.Map<Models.WineDTO>(result));
        }

        [HttpPost("winepriceupdate")]

        public async Task<ActionResult<PriceChange>> Post(PriceChange priceChange)
        {
            var wineToUpdate = await WineRepository.GetWineById(priceChange.WineId);
            wineToUpdate.Price = priceChange.Price;
            await WineRepository.SaveChanges();

            //if everything is ok, send an integration event

            //create message model

            PriceUpdateMessage message = new PriceUpdateMessage
            {
                Price = priceChange.Price,
                WineId = priceChange.WineId
            };

            try
            {
                await messageBus.PublishMessage(message, "priceupdatedmessage");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok(priceChange);
        }
    }
}
