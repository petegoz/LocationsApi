using System;
using System.Collections.Generic;
using System.Linq;
using Operations;

namespace Model.InMemoryDataAccess
{
    public class UserHistoryReader: IReader<IEnumerable<Location>>
    {
        private readonly LocationStore locationStore;

        public UserHistoryReader(LocationStore locationStore)
        {
            this.locationStore = locationStore;
        }

        public string UserId { get; set; }

        public Result<IEnumerable<Location>> Read()
        {
            try
            {
                var userLocations = locationStore.Where(location => location.UserId == UserId).ToList();
                if (!userLocations.Any())
                {
                    return Result<IEnumerable<Location>>.CreateFailureResult($"No locations found for user {UserId}");
                }
                var locations = userLocations.OrderByDescending(location => location.DateTime);
                return Result<IEnumerable<Location>>.CreateSuccessResult(locations, $"Locations found for user {UserId}");
            }
            catch (Exception exception)
            {
                return Result<IEnumerable<Location>>.CreateFailureResult($"UserHistoryReader: {exception.Message}", exception);
            }
        }
    }
}
