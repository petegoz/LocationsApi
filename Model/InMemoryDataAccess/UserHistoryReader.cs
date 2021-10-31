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

        public IEnumerable<Location> Read()
        {
            var userLocations = locationStore.Where(location => location.UserId == UserId);
            return userLocations.OrderByDescending(location => location.DateTime);
        }
    }
}
