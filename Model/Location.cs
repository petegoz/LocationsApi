using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Location
    {
        [Required][Range(-90, 90)]
        public double Latitude { get; set; }

        [Required][Range(-180, 180)]
        public double Longitude { get; set; }

        public string UserId { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
