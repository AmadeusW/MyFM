using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFm.Core
{
    public class State
    {
        public Location CurrentLocation { get; private set; }
        public List<Location> Locations { get; } = new List<Location>();

        public State()
        {

        }

        public void AddLocation(Location location)
        {
            Locations.Add(location);
        }

        public void SetCurrentLocation(string path)
        {
            CurrentLocation = new Location(path, false, false);
        }

        public IEnumerable<string> GetContents(Location location)
        {
            return Directory.EnumerateFileSystemEntries(location.Path);
        }


    }
}
