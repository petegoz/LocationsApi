using System;
using System.Linq;
using System.Net;
using Operations;

namespace Model.InMemoryDataAccess
{
    /// <summary>
    /// Get the latest location for a given user.
    /// </summary>
    public class UserLocationReader: IReader<Location>
    {
        private readonly LocationStore locationStore;

        public UserLocationReader(LocationStore locationStore)
        {
            this.locationStore = locationStore;
        }

        public string UserId { get; set; }

        public Result<Location> Read()
        {
            try
            {
                var userLocations = locationStore.Where(location => location.UserId == UserId).ToList();
                if (!userLocations.Any())
                {
                    return Result<Location>.CreateFailureResult($"No locations found for user {UserId}", HttpStatusCode.NotFound);
                }
                var currentLocation = userLocations.OrderByDescending(location => location.DateTime).FirstOrDefault();
                return Result<Location>.CreateSuccessResult(currentLocation, $"Current location found for user {UserId}");
            }
            catch (Exception exception)
            {
                var message = $"UserLocationReader: {exception.Message}";
                return Result<Location>.CreateFailureResult(message, HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
