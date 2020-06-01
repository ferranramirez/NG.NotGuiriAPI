using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NG.Common.Library.Filters;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Business.Contract;
using System;
using System.Collections.Generic;
using System.Net;

namespace NG.NotGuiriAPI.Presentation.WebAPI.Controllers
{
    [Authorize(Roles = "Basic, Standard, Premium, Admin")]
    [ApiController]
    [Route("[controller]")]
    public class NodeController : ControllerBase
    {
        private readonly INodeService _nodeService;

        public NodeController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        /// <summary>
        /// Retrieve the node by its Id
        /// </summary>
        /// <param name="Id">The Id of the desired Node</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully validated.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A Node</returns>

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Node), (int)HttpStatusCode.OK)]
        public IActionResult Get(Guid Id)
        {
            return Ok(_nodeService.Get(Id));
        }

        /// <summary>
        /// Retrieve the nodes by their tour Id
        /// </summary>
        /// <param name="TourId">The Id of the desired Tour</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully validated.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A List of Nodes</returns>
        [HttpGet("Tour/{TourId}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<Node>), (int)HttpStatusCode.OK)]
        public IActionResult GetNodes(Guid TourId)
        {
            return Ok(_nodeService.GetNodes(TourId));
        }

    }
}
