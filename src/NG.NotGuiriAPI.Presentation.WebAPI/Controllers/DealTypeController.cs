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
    public class DealTypeController : ControllerBase
    {
        private readonly IReadAllService<DealType> _dealTypeService;

        public DealTypeController(IReadAllService<DealType> dealTypeService)
        {
            _dealTypeService = dealTypeService;
        }

        /// <summary>
        /// Retrieve all DealTypes
        /// </summary>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - DealTypes successfully retrieved.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>All DealTypes</returns>

        [HttpGet]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(DealType), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dealTypeService.GetAll());
        }
    }
}
