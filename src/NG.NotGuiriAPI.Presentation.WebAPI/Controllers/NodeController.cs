using Microsoft.AspNetCore.Mvc;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Business.Contract;
using System;

namespace NG.NotGuiriAPI.Presentation.WebAPI.Controllers
{
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
        [ProducesResponseType(typeof(Node), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _nodeService.Get(Id);

            return Ok(response);
        }

    }
}
