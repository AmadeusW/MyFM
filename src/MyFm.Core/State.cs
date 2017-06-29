using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFm.Core
{
    class State
    {
        List<Location> Locations { get; } = new List<Location>();

        public State()
        {

        }

        public void AddLocation(Location location)
        {
            Locations.Add(location);
        }

        public IEnumerable<string> GetContents(Location location)
        {
            return Directory.EnumerateFileSystemEntries(location.Path);
        }


    }
}
