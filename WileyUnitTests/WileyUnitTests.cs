using WileyTest;

namespace WileyUnitTests
{
    public class WileyUnitTests : IDisposable
    {
        public string testPath;

        public WileyUnitTests()
        {
            testPath = Path.GetTempFileName();
        }

        static void WriteVersion(string path, string versionNumber)
        {
            File.WriteAllText(path, versionNumber);
        }

        [Fact]
        public void SetMinorVersionNumber_Successful()
        {
            WriteVersion(testPath, "1.0.0.0");
            VersionIterator versionIterator = new VersionIterator(testPath);
            versionIterator.IncrementMinor(1);
            versionIterator.Save();

            Assert.Equal("1.0.0.1", File.ReadAllText(testPath));
        }

        [Fact]
        public void SetMajorVersionNumber_Successful()
        {
            WriteVersion(testPath,"1.0.0.0");
            VersionIterator versionIterator = new VersionIterator(testPath);
            versionIterator.IncrementMajor(1);
            versionIterator.Save();

            Assert.Equal("1.0.1.0", File.ReadAllText(testPath));
        }

        [Fact]
        public void WrongVersionFormat_ThrowsException()
        {
            WriteVersion(testPath, "1.0.0");
            Assert.Throws<VersionParseError>(() => new VersionIterator(testPath));
        }

        [Fact]
        public void NoFile_ThrowsException()
        {
            File.Delete(testPath);
            Assert.Throws<VersionReadError>(() => new VersionIterator(testPath));
        }

        [Fact]
        public void NoVersionNumberFound_ThrowsException()
        {
            WriteVersion(testPath, "");
            Assert.Throws<NoVersionFound>(() => new VersionIterator(testPath));
        }

        [Fact]
        public void VersionWriteError_ThrowsException()
        {   
            WriteVersion(testPath, "1.0.0.0");
            File.SetAttributes(testPath, FileAttributes.ReadOnly);
            Assert.Throws<VersionWriteError>(() => {
                VersionIterator versionIterator = new VersionIterator(testPath);
                versionIterator.IncrementMinor(1);
                versionIterator.Save();
            });

            File.SetAttributes(testPath, FileAttributes.Normal);
        }

        public void Dispose()
        {
            if (File.Exists(testPath))
            {
                File.Delete(testPath);
            }
        }
    }
}