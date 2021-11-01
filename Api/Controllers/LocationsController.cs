using System.Collections;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Operations;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly CreateLocationCommand createLocationCommand;
        private readonly LocationsQuery locationsQuery;
        private readonly UserLocationQuery userLocationQuery;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(CreateLocationCommand createLocationCommand, LocationsQuery locationsQuery, UserLocationQuery userLocationQuery, ILogger<LocationsController> logger)
        {
            this.createLocationCommand = createLocationCommand;
            this.locationsQuery = locationsQuery;
            this.userLocationQuery = userLocationQuery;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> GetAllUsersLocations()
        {
            var result = locationsQuery.Run();
            return ToActionResult(result);
        }

        /// <summary>
        /// Get the latest location of a single user.
        /// </summary>
        /// <param name="userId">ID of an existing user.</param>
        /// <returns>Latest location of the user.</returns>
        [HttpGet]
        [Route("{userId}")]
        public ActionResult<Location> GetSingleUserLocation(string userId)
        {
            userLocationQuery.UserId = userId;
            var result = userLocationQuery.Run();
            return ToActionResult(result); 
        }

        [HttpGet]
        [Route("{userId}/history")]
        public ActionResult<IEnumerable<Location>> GetSingleUserLocationHistory(string userId)
        {
            locationsQuery.UserId = userId;
            var result = locationsQuery.Run();
            return ToActionResult(result);
        }

        /// <summary>
        /// Create a Location record from JSON data containing latitude and longitude in the request body.
        /// </summary>
        /// <param name="userId">The ID of an existing user.</param>
        /// <param name="location">JSON data containing latitude and longitude.</param>
        /// <returns>The newly saved location.</returns>
        [HttpPost]
        [Route("{userId}")]
        public ActionResult<Location> PostSingleUserLocation(string userId, [FromBody] Location location)
        {
            createLocationCommand.UserId = userId;
            createLocationCommand.Location = location;
            var result = createLocationCommand.Run();
            if (!result.Success)
            {
                return ToActionResult(result);
            }
            return CreatedAtAction(nameof(PostSingleUserLocation), result.Data);
        }

        [HttpGet]
        [Route("area")]
        public ActionResult<IEnumerable<Location>> GetAllUsersLocationsWithArea([FromQuery] double n, [FromQuery] double s, [FromQuery] double e, [FromQuery] double w)
        {
            locationsQuery.Area = new Area(n, s, e, w);
            var result = locationsQuery.Run();
            return ToActionResult(result);
        }

        private ActionResult<T> ToActionResult<T>(Result<T> result)
        {
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else if (result.Exception != null)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, result.Message);
            }
            else if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result.Message);
            }
            else if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(result.Message);
            }

            return StatusCode((int) result.StatusCode, result.Message);
        }
    }
}
