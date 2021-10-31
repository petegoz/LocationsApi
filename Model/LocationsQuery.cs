using System.Collections.Generic;
using Operations;

namespace Model
{
    /// <summary>
    /// Get current locations for all users.
    /// </summary>
    public class LocationsQuery: IQuery<IEnumerable<Location>>
    {
        private readonly LocationsReader locationsReader;

        public LocationsQuery(LocationsReader locationsReader)
        {
            this.locationsReader = locationsReader;
        }

        public IEnumerable<Location> Run()
        {
            return locationsReader.Read();
        }
    }
}
