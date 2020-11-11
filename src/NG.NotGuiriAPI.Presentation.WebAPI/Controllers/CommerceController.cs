using Microsoft.AspNetCore.Mvc;
using NG.Common.Library.Filters;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Business.Contract;
using System.Net;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Presentation.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommerceController : ControllerBase
    {
        private readonly IReadAllService<Commerce> _commerceService;

        public CommerceController(IReadAllService<Commerce> commerceService)
        {
            _commerceService = commerceService;
        }

        /// <summary>
        /// Retrieve all commerces
        /// </summary>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Commerces successfully retrieved.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>All Commerces</returns>

        [HttpGet]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Commerce), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _commerceService.GetAll());
        }
    }
}
