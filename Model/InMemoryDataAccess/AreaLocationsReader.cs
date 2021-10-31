using System.Collections.Generic;
using System.Linq;
using Operations;

namespace Model.InMemoryDataAccess
{
    public class AreaLocationsReader: IReader<IEnumerable<Location>>
    {
        private readonly LocationStore locationStore;

        public AreaLocationsReader(LocationStore locationStore)
        {
            this.locationStore = locationStore;
        }

        public Area Area { get; set; }

        public IEnumerable<Location> Read()
        {
            var locations = locationStore.Where(location => Area.Contains(location));

            // Group the locations by user:
            var userLocations = locations.GroupBy(location => location.UserId);

            // Take the current location for each user:
            return userLocations.Select(singleUserLocations => singleUserLocations.OrderByDescending(location => location.DateTime).FirstOrDefault()).ToList();
        }
    }
}
