using System;
using System.Net;
using Operations;

namespace Model.InMemoryDataAccess
{
    public class LocationWriter: IWriter<Location>
    {
        private readonly LocationStore locationStore;

        public LocationWriter(LocationStore locationStore)
        {
            this.locationStore = locationStore;
        }

        public Result<Location> Write()
        {
            try
            {
                locationStore.Add(Location);
                return Result<Location>.CreateSuccessResult(Location, "New location saved.");
            }
            catch (Exception exception)
            {
                var message = $"LocationWriter: {exception.Message}";
                return Result<Location>.CreateFailureResult(message, HttpStatusCode.InternalServerError, exception);
            }
        }

        public Location Location { get; set; }
    }
}
