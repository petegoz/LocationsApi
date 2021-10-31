using System;
using System.Collections.Generic;
using Model.InMemoryDataAccess;
using Operations;

namespace Model
{
    /// <summary>
    /// Get current locations for all users.
    /// </summary>
    public class LocationsQuery: IQuery<IEnumerable<Location>>
    {
        private readonly LocationsReader locationsReader;
        private readonly AreaLocationsReader areaLocationsReader;
        private readonly UserHistoryReader userHistoryReader;

        public LocationsQuery(LocationsReader locationsReader, AreaLocationsReader areaLocationsReader, UserHistoryReader userHistoryReader)
        {
            this.locationsReader = locationsReader;
            this.areaLocationsReader = areaLocationsReader;
            this.userHistoryReader = userHistoryReader;
        }

        public string UserId { get; set; }

        public Area Area { get; set; }

        public IEnumerable<Location> Run()
        {
            if (UserId != null && Area != null)
            {
                throw new NotImplementedException("Please specify area or user ID, not both.");
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                return userHistoryReader.Read();
            }
            if (Area != null)
            {
                return areaLocationsReader.Read();
            }
            return locationsReader.Read();
        }
    }
}
