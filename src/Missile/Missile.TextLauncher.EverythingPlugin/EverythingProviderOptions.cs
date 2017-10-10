using CommandLine;

namespace Missile.TextLauncher.EverythingPlugin
{
    public class EverythingProviderOptions
    {
        [Option('r', "regex", HelpText = "Indicates the search text is a regular expression")]
        public bool IsRegex { get; set; }

        [Option('l', "limit", HelpText = "Maximum number of search results to get")]
        public int? NumMaxResults { get; set; }
    }
}