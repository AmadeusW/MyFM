using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MyFm.Core.Contracts;

namespace MyFm.Core
{
    public class State
    {
        public Location CurrentLocation { get; private set; }
        public Dictionary<string, Location> Locations { get; } = new Dictionary<string, Location>();
        public bool Running { get; set; }

        public State()
        {
            Running = true;
        }

        public void AddLocation(Location location, string moniker)
        {
            Locations[moniker] = location;
        }

        public void SetCurrentLocation(string path)
        {
            CurrentLocation = new Location(path, false, false);
        }
    }
}
