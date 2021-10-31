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
            
            // Todo current locations only
            // return userLocations.OrderByDescending(location => location.DateTime);
            return locations;
        }
    }
}
