using System.Linq;
using Operations;

namespace Model
{
    public class LocationWriter: IWriter<Location>
    {
        private readonly LocationStore locationStore;

        public LocationWriter(LocationStore locationStore)
        {
            this.locationStore = locationStore;
        }

        public Location Write()
        {
            locationStore.Add(Location);
            return locationStore.FirstOrDefault(location => location.UserId == Location.UserId);
        }

        public Location Location { get; set; }
    }
}
