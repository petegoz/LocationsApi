﻿using System.Collections.Generic;
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

        public Result<IEnumerable<Location>> Read()
        {
            var areaLocations = locationStore.Where(location => Area.Contains(location));

            // Group the locations by user:
            var userLocations = areaLocations.GroupBy(location => location.UserId);

            // Take the current location for each user:
            var locations = userLocations.Select(singleUserLocations => singleUserLocations.OrderByDescending(location => location.DateTime).FirstOrDefault()).ToList();
            return Result<IEnumerable<Location>>.CreateSuccessResult(locations, "Current locations found in area.");
        }
    }
}
