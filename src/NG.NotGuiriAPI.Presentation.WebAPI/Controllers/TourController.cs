using Microsoft.AspNetCore.Mvc;
using NG.Common.Library.Filters;
using NG.DBManager.Infrastructure.Contracts.Entities;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.NotGuiriAPI.Business.Contract;
using NG.NotGuiriAPI.Domain;
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
        [ProducesResponseType(typeof(TourWithDealType), (int)HttpStatusCode.OK)]
        public IActionResult Get(Guid Id)
        {
            var response = _tourService.Get(Id);
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
        [ProducesResponseType(typeof(List<TourWithDealType>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFeatured()
        {
            return Ok(await _tourService.GetFeatured());
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
        [ProducesResponseType(typeof(List<TourWithDealType>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLastOnesCreated()
        {
            return Ok(await _tourService.GetLastOnesCreated());
        }

        /// <summary>
        /// Retrieve all the tours that have a tag that is exactly like the Filter string
        /// </summary>
        /// <param name="Filter">The tag we want to filter the tours by</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Tours successfully retrieved.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>A List of Tour</returns>
        [HttpGet("GetByTag/{Filter}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<TourWithDealType>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByTag(string Filter)
        {
            return Ok(await _tourService.GetByTag(Filter));
        }

        /// <summary>
        /// Retrieve all the tours that have a tag or its name containing the Filter string
        /// </summary>
        /// <param name="Filter">The partial tag or tour name we want to filter the tours by. It returns all the Tours if there's no filter.</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Tours successfully retrieved.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>
        /// A List of Tour
        /// </returns>
        [HttpGet("GetByTagOrName/{Filter?}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<TourWithDealType>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByTagOrName(string Filter = null)
        {
            return Ok(await _tourService.GetByTagOrName(Filter));
        }

        /// <summary>
        /// Retrieve all the tours that have a node correspongind the given Commerce Name
        /// </summary>
        /// <param name="Filter">The commerce name we want to filter the tours by. It returns all the Tours if there's no filter.</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Tours successfully retrieved.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>
        /// A List of Tour
        /// </returns>
        [HttpGet("GetByCommerceName/{Filter?}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<TourWithDealType>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCommerceName(string Filter = null)
        {
            return Ok(await _tourService.GetByCommerceName(Filter));
        }

        /// <summary>
        /// Retrieve all the tours that have a node that contains a deal from the given DealType
        /// </summary>
        /// <param name="Filter">The dealtype we want to filter the tours by. It returns all the Tours if there's no filter.</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Tours successfully retrieved.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>
        /// A List of Tour
        /// </returns>
        [HttpGet("GetByDealType/{Filter?}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<TourWithDealType>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByDealType(string Filter = null)
        {
            return Ok(await _tourService.GetByDealType(Filter));
        }

        /// <summary>
        /// Retrieve all the tours that have a Deal, Node, DealType, etc that matches the filter.
        /// </summary>
        /// <param name="Filter">The filter we want to filter the tours by. It returns all the Tours if there's no filter.</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Tours successfully retrieved.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>
        /// A List of Tour
        /// </returns>
        [HttpGet("GetByEverything/{Filter?}")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<TourWithDealType>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByEverything(string Filter = null)
        {
            return Ok(await _tourService.GetByEverything(Filter));
        }

        /// <summary>
        /// Retrieve all the tours that start in the given radius from the given coordinates.
        /// </summary>
        /// <param name="Location">The coordinates and the radius (in meters) of the area to get the tours from.</param>
        /// <remarks>
        /// ## Response code meanings
        /// - 200 - Tours successfully retrieved.
        /// - 500 - An internal server error. Something bad and unexpected happened.
        /// - 543 - A handled error. This error was expected, check the message.
        /// </remarks>
        /// <returns>
        /// A List of Tour
        /// </returns>
        [HttpPost("GetByDistance")]
        [ProducesResponseType(typeof(ApiError), 543)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(List<TourWithDealType>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByDistance(LocationRequest Location)
        {
            return Ok(await _tourService.GetByDistance(Location));
        }
    }
}
