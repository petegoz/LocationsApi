using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(ILogger<LocationsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public OkObjectResult GetAllUsersLocations()
        {
            return Ok("GetAllUsersLocations");
        }

        [HttpGet]
        [Route("{userId}")]
        public OkObjectResult GetSingleUserLocation(string userId)
        {
            return Ok("GetSingleUserLocation");
        }

        [HttpGet]
        [Route("{userId}/history")]
        public OkObjectResult GetSingleUserLocationHistory(string userId)
        {
            return Ok("GetSingleUserLocationHistory");
        }

        /// <summary>
        /// Create a Location record from JSON data containing latitude and longitude in the request body.
        /// </summary>
        /// <param name="userId">The ID of an existing user.</param>
        /// <param name="location">JSON data containing latitude and longitude.</param>
        /// <returns>Todo return the new location</returns>
        [HttpPost]
        [Route("{userId}")]
        public OkObjectResult PostSingleUserLocation(string userId, [FromBody] Location location)
        {
            return Ok($"PostSingleUserLocation {userId} {location.Latitude} {location.Longitude}");
        }

        [HttpGet]
        [Route("area")]
        public OkObjectResult GetAllUsersLocationsWithArea([FromQuery] double n, [FromQuery] double e, [FromQuery] double s, [FromQuery] double w)
        {
            return Ok($"GetAllUsersLocationsWithArea {n} {e} {s} {w}");
        }
    }
}
