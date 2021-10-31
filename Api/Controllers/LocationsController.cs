using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly CreateLocationCommand createLocationCommand;
        private readonly LocationsQuery locationsQuery;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(CreateLocationCommand createLocationCommand, LocationsQuery locationsQuery, ILogger<LocationsController> logger)
        {
            this.createLocationCommand = createLocationCommand;
            this.locationsQuery = locationsQuery;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> GetAllUsersLocations()
        {
            var locations = locationsQuery.Run();
            return Ok(locations);
        }

        [HttpGet]
        [Route("{userId}")]
        public OkObjectResult GetSingleUserLocation(string userId)
        {
            // todo
            return Ok("GetSingleUserLocation");
        }

        [HttpGet]
        [Route("{userId}/history")]
        public OkObjectResult GetSingleUserLocationHistory(string userId)
        {
            // todo
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
        public ActionResult<Location> PostSingleUserLocation(string userId, [FromBody] Location location)
        {
            createLocationCommand.UserId = userId;
            createLocationCommand.Location = location;
            var storedLocation = createLocationCommand.Run();
            return CreatedAtAction(nameof(PostSingleUserLocation), storedLocation);
        }

        [HttpGet]
        [Route("area")]
        public ActionResult<IEnumerable<Location>> GetAllUsersLocationsWithArea([FromQuery] double n, [FromQuery] double e, [FromQuery] double s, [FromQuery] double w)
        {
            // todo
            return Ok($"GetAllUsersLocationsWithArea {n} {e} {s} {w}");
        }
    }
}
