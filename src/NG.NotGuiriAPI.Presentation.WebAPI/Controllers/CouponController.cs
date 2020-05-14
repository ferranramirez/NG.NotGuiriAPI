using Microsoft.AspNetCore.Mvc;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Business.Contract;

namespace NG.NotGuiriAPI.Presentation.WebAPI.Controllers
{
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
        /// - 200 - Coupon successfully generated
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// </remarks>
        /// <returns>A bool</returns>
        [HttpPost("{Id}")]
        [ProducesResponseType(typeof(Coupon), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Add([FromBody] Coupon NewCoupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _couponService.Add(NewCoupon);

            return Ok(response);
        }

    }
}
