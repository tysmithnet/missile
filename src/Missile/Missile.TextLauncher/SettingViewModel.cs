using System.Windows;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     View model for an individual setting
    ///     For example, search settings might have a max depth setting
    /// </summary>
    public class SettingViewModel
    {
        /// <summary>
        ///     Gets or sets the name
        /// </summary>
        /// <value>
        ///     The name
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the property editor that will allow for this setting to be changed
        /// </summary>
        /// <value>
        ///     The property editor
        /// </value>
        public FrameworkElement PropertyEditor { get; set; }
    }
}