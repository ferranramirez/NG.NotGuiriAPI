using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NG.Common.Library.Filters;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Business.Contract;
using System;
using System.Net;

namespace NG.NotGuiriAPI.Presentation.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieve the User information by its mail contained in the token
        /// </summary>
        /// <param name="UserId">This value is ignored. The userId is constructed from the authorization token</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully validated.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A User</returns>
        [Authorize]
        [AuthUserIdFromToken]
        [HttpGet]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public IActionResult Get(Guid AuthUserId = default /* Got from the [AuthUserIdFromToken] filter */ )
        {
            return Ok(_userService.Get(AuthUserId));
        }
    }
}
