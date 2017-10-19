using CommandLine;

namespace Missile.TextLauncher.EverythingPlugin
{
    /// <summary>
    ///     Options for EverythingProvider
    /// </summary>
    public class EverythingProviderOptions
    {
        /// <summary>
        ///     Gets or sets a value indicating whether the search text is a regular expression
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is regex; otherwise, <c>false</c>.
        /// </value>
        [Option('r', "regex", HelpText = "Indicates the search text is a regular expression")]
        public bool IsRegex { get; set; }

        /// <summary>
        ///     Gets or sets the number maximum results
        /// </summary>
        /// <value>
        ///     The number maximum results
        /// </value>
        [Option('n', "num", HelpText = "Maximum number of search results to get")]
        public int? NumMaxResults { get; set; }
    }
}