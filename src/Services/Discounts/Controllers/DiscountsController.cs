using AutoMapper;
using Discounts.Models;
using Discounts.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discounts.Controllers
{
    [ApiController]
    [Route("api/discounts")]
    public class DiscountsController : Controller
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public DiscountsController(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

        // TODO: get discount for user!! 

        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDiscountForUser(Guid userId)
        {
            var coupon = await _couponRepository.GetCouponByUserId(userId);

            if (coupon == null)
                return NotFound();

            return Ok(_mapper.Map<CouponDto>(coupon));
        }

        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("{couponId}")]
        public async Task<IActionResult> GetDiscountForCode(Guid couponId)
        {
            var coupon = await _couponRepository.GetCouponById(couponId);

            if (coupon == null)
                return NotFound();

            return Ok(_mapper.Map<CouponDto>(coupon));
        }
    }
}
