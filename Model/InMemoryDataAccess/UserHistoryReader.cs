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
            var userLocations = locationStore.Where(location => location.UserId == UserId);
            var locations = userLocations.OrderByDescending(location => location.DateTime);
            return Result<IEnumerable<Location>>.CreateSuccessResult(locations, $"Locations found for user {UserId}");
        }
    }
}
