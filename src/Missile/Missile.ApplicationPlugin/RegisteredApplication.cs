using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

namespace Missile.ApplicationPlugin
{
    /// <summary>
    ///     Represents an application that has been registered with the application
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RegisteredApplication
    {
        /// <summary>
        ///     Gets or sets the icon of the application
        /// </summary>
        /// <value>
        ///     The icon
        /// </value>
        public ImageSource Icon { get; set; }

        /// <summary>
        ///     Gets or sets the name of the application.
        /// </summary>
        /// <value>
        ///     The name of the application
        /// </value>
        public string ApplicationName { get; set; }

        /// <summary>
        ///     Gets or sets the application path
        /// </summary>
        /// <value>
        ///     The application path
        /// </value>
        public string ApplicationPath { get; set; }
    }
}