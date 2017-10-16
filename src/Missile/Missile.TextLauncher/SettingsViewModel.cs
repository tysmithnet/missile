using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Adapter for ISettings that is more framework friendly
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SettingsViewModel
    {
        /// <summary>
        ///     Gets or sets the user friendly name for the provided settings instance
        /// </summary>
        /// <value>
        ///     The user friendly name for the provided settings instance
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the settings instance
        /// </summary>
        /// <value>
        ///     The settings instance
        /// </value>
        public object Instance { get; set; }

        /// <summary>
        ///     Gets or sets the sub settings
        /// </summary>
        /// <value>
        ///     The sub settings
        /// </value>
        public IList<SettingsViewModel> SubSettings { get; set; } = new List<SettingsViewModel>();

        /// <summary>
        ///     Gets or sets the individual settings
        /// </summary>
        /// <value>
        ///     The individual settings
        /// </value>
        public IList<SettingViewModel> Settings { get; set; } = new List<SettingViewModel>();
    }
}