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
        public OkObjectResult Get()
        {
            return Ok("API Working");
        }
    }
}
