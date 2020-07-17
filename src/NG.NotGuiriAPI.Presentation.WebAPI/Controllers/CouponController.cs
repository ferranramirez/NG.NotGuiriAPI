using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NG.Common.Library.Filters;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Business.Contract;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Presentation.WebAPI.Controllers
{
    [Authorize(Roles = "Basic, Standard, Premium, Admin")]
    [ApiController]
    [Route("[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        /// <summary>
        /// Generates a new Coupon
        /// </summary>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully validated.
        /// - 400 - The model is not properly built.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A bool</returns>
        [AuthUserIdFromToken]
        [HttpPost("{NodeId}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAsync([FromQuery] string Content, Guid NodeId,
            Guid AuthUserId = default /* Got from the [AuthUserIdFromToken] filter */)
        {
            return Ok(await _couponService.Add(AuthUserId, NodeId, Content));
        }
    }
}
