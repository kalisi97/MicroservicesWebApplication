using AutoMapper;
using ShoppingBasket.Messages;
using ShoppingBasket.Models;
using ShoppingBasket.Repositories;
using ShoppingBasket.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MessagingBus;
using ShoppingBasket.Helpers;
using Microsoft.AspNetCore.Authentication;

namespace ShoppingBasket.Controllers
{
    [Route("api/baskets")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;
        private readonly IMessageBus messageBus;
        private readonly IDiscountService discountService;
        private readonly TokenExchangeService tokenExchangeService;

        public BasketsController(IBasketRepository basketRepository, 
            IMapper mapper, 
            IMessageBus messageBus, 
            IDiscountService discountService,
            TokenExchangeService tokenExchangeService)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
            this.messageBus = messageBus;
            this.discountService = discountService;
            this.tokenExchangeService = tokenExchangeService;
        }

        [HttpGet("{basketId}", Name = "GetBasket")]
        public async Task<ActionResult<Basket>> Get(Guid basketId)
        {
            try
            {
                var basket = await basketRepository.GetBasketById(basketId);
                if (basket == null)
                {
                    return NotFound();
                }
                Coupon coupon = null;
                var userId = basket.UserId;
                if (!(userId == Guid.Empty))
                    coupon = await discountService.GetCoupon(userId);

                var result = mapper.Map<Basket>(basket);
                result.NumberOfItems = basket.BasketLines.Sum(bl => bl.Quantity);
                if (coupon != null)
                {
                    result.CouponId = coupon.CouponId;
                }
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);

            }
        }


        [HttpGet("discount/{userId}", Name = "GetDiscountForBasket")]

        public async Task<ActionResult<Coupon>> GetDiscountForBasket(Guid userId)
        {
            try
            {
            
                Coupon coupon = null;

                if (!(userId == Guid.Empty))
                    coupon = await discountService.GetCoupon(userId);

             
                return Ok(coupon);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);

            }
        }

      
        [HttpGet("user/{userId}", Name = "GetBasketForUser")]
        public async Task<ActionResult<Basket>> GetBasketForUser(Guid userId)
        {
            try
            {
                var basket = await basketRepository.GetBasketByUserId(userId);
                if (basket == null)
                {
                    await Post(new BasketForCreation { UserId = userId});
                    basket = await basketRepository.GetBasketByUserId(userId);
                }
                Coupon coupon = null;
                
                if (!(userId == Guid.Empty))
                    coupon = await discountService.GetCoupon(userId);

                var result = mapper.Map<Basket>(basket);
                result.NumberOfItems = basket.BasketLines.Sum(bl => bl.Quantity);
                if (coupon != null)
                {
                    result.CouponId = coupon.CouponId;
                }
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);

            }
        }





        [HttpPost]
        public async Task<ActionResult<Basket>> Post(BasketForCreation basketForCreation)
        {
            var basketEntity = mapper.Map<Entities.Basket>(basketForCreation);

            basketRepository.AddBasket(basketEntity);
            await basketRepository.SaveChanges();

            var basketToReturn = mapper.Map<Basket>(basketEntity);

            return CreatedAtRoute(
                "GetBasket",
                new { basketId = basketEntity.BasketId },
                basketToReturn);
        }
         

        [HttpPost("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CheckoutBasketAsync([FromBody] BasketCheckout basketCheckout)
        {
            try
            {
             
                var basket = await basketRepository.GetBasketById(basketCheckout.BasketId);

                if (basket == null)
                {
                    return BadRequest();
                }

                var basketCheckoutMessage = mapper.Map<BasketCheckoutMessage>(basketCheckout);
                basketCheckoutMessage.BasketLines = new List<BasketLineMessage>();
                decimal total = 0;

                foreach (var b in basket.BasketLines)
                {
                    var basketLineMessage = new BasketLineMessage
                    {
                        BasketLineId = b.BasketLineId,
                        Price = b.Price,
                        Quantity = b.Quantity
                    };

                    total += b.Price * b.Quantity;

                    basketCheckoutMessage.BasketLines.Add(basketLineMessage);
                }

                //apply discount by talking to the discount service
                Coupon coupon = null;

                // IRL, get the user id from this.User object
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c=>c.Type == "sub")?.Value);

                if (!(userId == Guid.Empty))
                    coupon = await discountService.GetCoupon(userId);

                if (coupon != null)
                {
                    if (coupon.AlreadyUsed == false)
                    {
                        basketCheckoutMessage.BasketTotal = total - coupon.Amount;
                        await discountService.ChangeCouponStatus(coupon);
                    }
                    else
                    {
                        basketCheckoutMessage.BasketTotal = total;
                    }
                }
                else
                {
                    basketCheckoutMessage.BasketTotal = total;
                }

                var incomingToken = await HttpContext.GetTokenAsync("access_token");
                var accessTokenForOrderingService = await tokenExchangeService
                    .GetTokenAsync(incomingToken, "ordering.fullaccess");

                basketCheckoutMessage.SecurityContext.AccessToken = accessTokenForOrderingService;
                try
                {
                    //message count should be 1 
                    //the subscription has received the message that you sent to the topic, but no receiver has picked them yet.
                    await messageBus.PublishMessage(basketCheckoutMessage, "checkoutmessage");
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);

                }

                await basketRepository.ClearBasket(basketCheckout.BasketId);
                return Accepted(basketCheckoutMessage);
                //message count should be 0
                //on the Service Bus Subscription page, the Active message count is 0.
                //It's because a receiver has received messages from this subscription and completed the messages.
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);
            }
        }

        /*
        //added for testing purpose
        [HttpPost("testcheckout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> TestCheckoutBasketAsync([FromBody] BasketCheckout basketCheckout)
        {
            try
            {
              
                var basket = await basketRepository.GetBasketById(basketCheckout.BasketId);

                if (basket == null)
                {
                    return BadRequest();
                }

                var basketCheckoutMessage = mapper.Map<BasketCheckoutMessage>(basketCheckout);
                basketCheckoutMessage.BasketLines = new List<BasketLineMessage>();
                decimal total = 0;

                foreach (var b in basket.BasketLines)
                {
                    var basketLineMessage = new BasketLineMessage
                    {
                        BasketLineId = b.BasketLineId,
                        Price = b.Price,
                        Quantity = b.Quantity
                    };

                    total += b.Price * b.Quantity;

                    basketCheckoutMessage.BasketLines.Add(basketLineMessage);
                }


                Coupon coupon = null;

 
                var userId = basketCheckout.UserId;

                if (!(userId == Guid.Empty))
                    coupon = await discountService.GetCoupon(userId);

                if (coupon != null)
                {
                    basketCheckoutMessage.BasketTotal = total - coupon.Amount;
                }
                else
                {
                    basketCheckoutMessage.BasketTotal = total;
                }

                await basketRepository.ClearBasket(basketCheckout.BasketId);
             
                return Ok("Checkout created!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);
            }
        }
        */
        }
}
