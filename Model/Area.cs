namespace Model
{
    public class Area
    {
        private readonly double north;
        private readonly double south;
        private readonly double east;
        private readonly double west;

        public Area(double north, double south, double east, double west)
        {
            this.north = north;
            this.south = south;
            this.east = east;
            this.west = west;
        }

        public bool Contains(Location location)
        {
            return location.Latitude <= north && location.Latitude >= south 
                && location.Longitude <= east &&  location.Longitude >= west;
        }
    }
}
