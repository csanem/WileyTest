using System.Text.RegularExpressions;

namespace WileyTest
{
    public class VersionIterator
    {
        private StreamReader reader;
        private int major;
        private int minor;
        private string prefix;
        private string filePath;

        /// <summary>
        /// Reads the file and parses the version number.
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="NoVersionFound">This is thrown when file has no text.</exception>
        /// <exception cref="VersionParseError">This is thrown when no valid version is found.</exception>
        /// <exception cref="VersionReadError">This is thrown if a file cannot be read.</exception>
        public VersionIterator(string filePath = "ProductInfo.cs")
        {
            this.filePath = filePath;

            try
            {
                reader = new StreamReader(this.filePath);
                string? version = reader.ReadLine();

                if (version == null)
                {
                    throw new NoVersionFound();
                }

                string regexPattern = @"^(\d+\.\d+)\.(\d+)\.(\d+)$";
                Match match = Regex.Match(version, regexPattern);

                if (!match.Success)
                {
                    throw new VersionParseError();
                }
                
                prefix = match.Groups[1].Value;
                major = int.Parse(match.Groups[2].Value);
                minor = int.Parse(match.Groups[3].Value);
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    throw new VersionReadError();
                }
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public void IncrementMajor(int increment)
        {
            SetMajor(major + increment);
            SetMinor(0);
        }

        public void IncrementMinor(int increment)
        {
            SetMinor(minor + increment);
        }

        private void SetMajor(int major)
        {
            this.major = major;
        }

        private void SetMinor(int minor)
        {
            this.minor = minor;
        }

        /// <summary>
        /// Saves the new version to the file.
        /// </summary>
        /// <returns>New version number as string</returns>
        /// <exception cref="VersionWriteError">This is thrown when the file cannot be written</exception>
        public string Save()
        {
            string newVersion = $"{prefix}.{major}.{minor}";
            StreamWriter? sw = null;
            try
            {
                sw = new StreamWriter(filePath, false);
                sw.Write(newVersion);
                return newVersion;
            }
            catch
            {
                throw new VersionWriteError();
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}