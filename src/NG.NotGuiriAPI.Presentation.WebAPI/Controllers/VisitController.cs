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
    [ApiController]
    [Route("[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        /// <summary>
        /// Add a new Visit into de database
        /// </summary>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Visit successfully added.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>The id of the new Visit</returns>
        [Authorize]
        [AuthUserIdFromToken]
        [HttpPost("{CommerceId}/{TourId}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Visit), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddAsync(Guid CommerceId, Guid TourId,
            Guid AuthUserId = default /* Got from the [AuthUserIdFromToken] filter */ )
        {
            var visitId = await _visitService.Add(AuthUserId, CommerceId, TourId);
            return Ok(visitId);
        }
    }
}
