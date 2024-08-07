namespace WileyTest
{
    public class VersionParseError : Exception
    {
        public VersionParseError() : base("Could not parse version number") { }
    }

    public class NoVersionFound : Exception
    {
        public NoVersionFound() : base("No version number found in file") { }
    }

    public class VersionReadError : Exception
    {
        public VersionReadError() : base("Unable to read the file") { }
    }

    public class VersionWriteError : Exception
    {
        public VersionWriteError() : base("Unable to write version to file") { }
    }
}