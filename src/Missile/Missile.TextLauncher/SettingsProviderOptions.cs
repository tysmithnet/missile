using CommandLine;

namespace Missile.TextLauncher
{
    /// <summary>
    /// Options for SettingsProvider
    /// </summary>
    public class SettingsProviderOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether the current settings values should be saved
        /// </summary>
        /// <value>
        ///   <c>true</c> if setting shoudl be saved; otherwise, <c>false</c>.
        /// </value>
        [Option('s', "save", HelpText = "Save the current settings")]
        public bool Save { get; set; }
    }
}