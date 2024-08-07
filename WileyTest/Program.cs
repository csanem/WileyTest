using WileyTest;

namespace WileyTest
{
    class Program
    {
        public const string FEATURE_ARGUMENT = "Feature";
        public const string BUG_FIX_ARGUMENT = "Bug fix";

        static void Main(string[] args)
        {
            string[] validArguments = { "feature", "bug fix" };
            string argument = string.Join(" ", args);

            if (args.Length == 0 || !validArguments.Contains(argument, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine("You need to provide either \"Bug Fix\" or \"Feature\"!");
                return;
            }

            try
            {
                VersionIterator versionIterator = new VersionIterator();

                if (argument.Equals(BUG_FIX_ARGUMENT, StringComparison.OrdinalIgnoreCase))
                {
                    versionIterator.IncrementMinor(1);
                }
                else if (argument.Equals(FEATURE_ARGUMENT, StringComparison.OrdinalIgnoreCase))
                {
                    versionIterator.IncrementMajor(1);
                }

                string newVersion = versionIterator.Save();
                Console.WriteLine($"Version number is updated successfully! New version: {newVersion}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}