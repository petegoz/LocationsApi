using System;

namespace Model
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }
    }
}
