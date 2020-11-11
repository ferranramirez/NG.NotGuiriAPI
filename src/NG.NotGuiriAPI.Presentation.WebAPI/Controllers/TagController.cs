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
    public class TagController : ControllerBase
    {
        private readonly IReadAllService<Tag> _tagService;

        public TagController(IReadAllService<Tag> tagService)
        {
            _tagService = tagService;
        }

        /// <summary>
        /// Retrieve all tags
        /// </summary>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Tags successfully retrieved.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>All Tags</returns>

        [HttpGet]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Tag), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tagService.GetAll());
        }
    }
}
