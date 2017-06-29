using System;

namespace MyFm.Core
{
    public class Location
    {
        public string Path { get; }
        public bool IsSource { get; }
        public bool IsDestination { get; }

        public Location(string path, bool isSource, bool isDestination)
        {
            Path = path;
            IsSource = isSource;
            IsDestination = isDestination;
        }

        public Location WithPath(string path) =>
            new Location(path, IsSource, IsDestination);
        public Location AsSource(bool isSource) =>
            new Location(Path, isSource, IsDestination);
        public Location AsDestination(bool isDestination) =>
            new Location(Path, IsSource, isDestination);
    }
}
