using Operations;

namespace Model.InMemoryDataAccess
{
    public class LocationWriter: IWriter<Location>
    {
        private readonly LocationStore locationStore;

        public LocationWriter(LocationStore locationStore)
        {
            this.locationStore = locationStore;
        }

        public Result<Location> Write()
        {
            locationStore.Add(Location);
            return Result<Location>.CreateSuccessResult(Location, "New location saved.");
        }

        public Location Location { get; set; }
    }
}
