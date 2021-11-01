using System;
using System.Collections.Generic;
using System.Linq;
using Operations;

namespace Model.InMemoryDataAccess
{
    /// <summary>
    /// Read current locations for all users.
    /// </summary>
    public class LocationsReader: IReader<IEnumerable<Location>>
    {
        private readonly LocationStore locationStore;

        public LocationsReader(LocationStore locationStore)
        {
            this.locationStore = locationStore;
        }

        public Result<IEnumerable<Location>> Read()
        {
            try
            {
                // Group the locations by user:
                var userLocations = locationStore.GroupBy(location => location.UserId);

                // Take the current location for each user:
                var locations = userLocations.Select(singleUserLocations => 
                    singleUserLocations.OrderByDescending(location => location.DateTime).FirstOrDefault()).ToList();

                return Result<IEnumerable<Location>>.CreateSuccessResult(locations, "Current locations found.");
            }
            catch (Exception exception)
            {
                return Result<IEnumerable<Location>>.CreateFailureResult($"LocationsReader: {exception.Message}", exception);
            }
        }
    }
}
