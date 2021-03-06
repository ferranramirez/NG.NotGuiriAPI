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
    [Authorize]
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
        /// <returns>The coupon generated</returns>
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

        /// <summary>
        /// Retrieves a Coupon
        /// </summary>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully retrieved.
        /// - 400 - The model is not properly built.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A coupon</returns>
        [HttpGet("{CouponId}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public IActionResult Get(Guid CouponId)
        {
            return Ok(_couponService.Get(CouponId));
        }

        /// <summary>
        /// Retrieves the Last Coupon for the given node
        /// </summary>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully retrieved.
        /// - 400 - The model is not properly built.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A coupon</returns>
        [AuthUserIdFromToken]
        [HttpGet("Last/{NodeId}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid NodeId,
            Guid AuthUserId = default /* Got from the [AuthUserIdFromToken] filter */)
        {
            return Ok(await _couponService.GetLastByNodeFromUser(AuthUserId, NodeId));
        }
    }
}
