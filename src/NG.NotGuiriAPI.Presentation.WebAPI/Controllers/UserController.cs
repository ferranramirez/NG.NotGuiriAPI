using Microsoft.AspNetCore.Mvc;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Business.Contract;
using System;

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
        /// Retrieve the User by its Id
        /// </summary>
        /// <param name="Id">The Id of the desired User</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Tour successfully retrieved
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// </remarks>
        /// <returns>A User</returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _userService.Get(Id);

            return Ok(response);
        }

    }
}
