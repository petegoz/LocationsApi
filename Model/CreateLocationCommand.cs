﻿using Operations;

namespace Model
{
    public class CreateLocationCommand: ICommand<Location>
    {
        private readonly LocationWriter locationWriter;

        public CreateLocationCommand(LocationWriter locationWriter)
        {
            this.locationWriter = locationWriter;
        }

        public string UserId { get; set; }
        public Location Location { get; set; }

        public Location Run()
        {
            Location.UserId = UserId;
            locationWriter.Location = Location;
            return locationWriter.Write();
        }

    }
}
