using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpPost]
        public OkObjectResult PostSingleUserLocation()
        {
            return Ok("PostSingleUserLocation");
        }

        [HttpGet]
        [Route("area")]
        public OkObjectResult GetAllUsersLocationsWithArea(double n, double e, double s, double w)
        {
            return Ok("GetAllUsersLocationsWithArea");
        }
    }
}
