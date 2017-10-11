using CommandLine;

namespace Missile.TextLauncher.Provision.Range
{
    /// <summary>
    ///     Options for RangeProvider instances
    /// </summary>
    public class RangeProviderOptions
    {
        /// <summary>
        ///     Gets or sets the starting value for the range
        /// </summary>
        /// <value>
        ///     The starting value for the range
        /// </value>
        [Option('s', "start", HelpText = "Starting value", DefaultValue = 0)]
        public int Start { get; set; }

        /// <summary>
        ///     Gets or sets the ending value of the range
        /// </summary>
        /// <value>
        ///     The ending value of the range
        /// </value>
        [Option('e', "end", HelpText = "Ending value", Required = true)]
        public int End { get; set; }

        /// <summary>
        ///     Gets or sets the increment that is added each iteration
        /// </summary>
        /// <value>
        ///     The increment that is added each iteration
        /// </value>
        [Option('i', "increment", HelpText = "How much should be added each iteration", DefaultValue = 1)]
        public int Increment { get; set; }
    }
}