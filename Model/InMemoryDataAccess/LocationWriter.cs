using System;
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
                return Result<Location>.CreateFailureResult($"LocationWriter: {exception.Message}", exception);
            }
        }

        public Location Location { get; set; }
    }
}
