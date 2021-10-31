using System.Linq;
using Operations;

namespace Model.InMemoryDataAccess
{
    /// <summary>
    /// Get the latest location for a given user.
    /// </summary>
    public class UserLocationReader: IReader<Location>
    {
        private readonly LocationStore locationStore;

        public UserLocationReader(LocationStore locationStore)
        {
            this.locationStore = locationStore;
        }

        public string UserId { get; set; }

        public Location Read()
        {
            var userLocations = locationStore.Where(location => location.UserId == UserId);
            return userLocations.OrderByDescending(location => location.DateTime).FirstOrDefault();
        }
    }
}
