using System.Collections.Generic;
using Operations;

namespace Model
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

        public IEnumerable<Location> Read()
        {
            // todo latest only for each user
            return locationStore;
        }
    }
}
