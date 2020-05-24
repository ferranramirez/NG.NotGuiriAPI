using Microsoft.AspNetCore.Mvc;
using NG.Common.Filters;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Business.Contract;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NG.NotGuiriAPI.Presentation.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TourController : ControllerBase
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        /// <summary>
        /// Retrieve a Tour by its tour Id
        /// </summary>
        /// <param name="Id">The Id of the desired Tour</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully validated.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A Tour</returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Tour), (int)HttpStatusCode.OK)]
        public IActionResult Get(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _tourService.Get(Id);

            return Ok(response);
        }

        /// <summary>
        /// Retrieve the nodes by their tour Id
        /// </summary>
        /// <param name="Id">The Id of the desired Tour</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully validated.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A List of Nodes</returns>
        [HttpGet("Nodes/{Id}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<Node>), (int)HttpStatusCode.OK)]
        public IActionResult GetNodes(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _tourService.GetNodes(Id);

            return Ok(response);
        }

        /// <summary>
        /// Retrieve all the tours set as featured
        /// </summary>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully validated.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A List of Tour</returns>
        [HttpGet("GetFeatured")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<Tour>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFeatured()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _tourService.GetFeatured();

            return Ok(response);
        }

        /// <summary>
        /// Retrieve the last 5 tours created
        /// </summary>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully validated.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A List of Tour </returns>
        [HttpGet("GetLastOnesCreated")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<Tour>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLastOnesCreated()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _tourService.GetLastOnesCreated();

            return Ok(response);
        }

        /// <summary>
        /// Retrieve all the tours that have the tag FullTag
        /// </summary>
        /// <param name="FullTag">The explicit tag we want to filter the tours by</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully validated.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A List of Tour</returns>
        [HttpGet("GetByFullTag/{FullTag}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<Tour>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByFullTag(string FullTag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _tourService.GetByFullTag(FullTag);

            return Ok(response);
        }

        /// <summary>
        /// Retrieve all the tours that have a tag that contains the Filter string
        /// </summary>
        /// <param name="Filter">The partial tag we want to filter the tours by</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully validated.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A List of Tour</returns>
        [HttpGet("GetByTag/{Filter}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<Tour>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByTag(string Filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _tourService.GetByTag(Filter);

            return Ok(response);
        }


        /// <summary>
        /// Retrieve all the tours that have a tag or its name containing the Filter string
        /// </summary>
        /// <param name="Filter">The partial tag or tour name we want to filter the tours by</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Coupon successfully validated.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>
        /// A List of Tour
        /// </returns>
        [HttpGet("GetByTagOrName/{Filter}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<Tour>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByTagOrName(string Filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _tourService.GetByTagOrName(Filter);

            return Ok(response);
        }
    }
}
