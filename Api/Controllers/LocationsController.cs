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
            var locations = locationsQuery.Run();
            return Ok(locations);
        }

        /// <summary>
        /// Get the latest location of a single user.
        /// </summary>
        /// <param name="userId">ID of an existing user.</param>
        /// <returns>Latest location of the user.</returns>
        [HttpGet]
        [Route("{userId}")]
        public OkObjectResult GetSingleUserLocation(string userId)
        {
            userLocationQuery.UserId = userId;
            var location = userLocationQuery.Run();
            return Ok(location);
        }

        [HttpGet]
        [Route("{userId}/history")]
        public OkObjectResult GetSingleUserLocationHistory(string userId)
        {
            locationsQuery.UserId = userId;
            var locations = locationsQuery.Run();
            return Ok(locations);
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
            var storedLocation = createLocationCommand.Run();
            return CreatedAtAction(nameof(PostSingleUserLocation), storedLocation);
        }

        [HttpGet]
        [Route("area")]
        public ActionResult<IEnumerable<Location>> GetAllUsersLocationsWithArea([FromQuery] double n, [FromQuery] double s, [FromQuery] double e, [FromQuery] double w)
        {
            locationsQuery.Area = new Area(n, s, e, w);
            var locations = locationsQuery.Run();
            return Ok(locations);
        }
    }
}
