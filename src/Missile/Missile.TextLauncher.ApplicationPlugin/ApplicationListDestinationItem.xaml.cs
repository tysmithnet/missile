using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using Missile.TextLauncher.ListPlugin;

namespace Missile.TextLauncher.ApplicationPlugin
{
    /// <summary>
    ///     A list destination item for applications
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    /// <seealso cref="Missile.TextLauncher.ListPlugin.IListDestinationItem" />
    public partial class ApplicationListDestinationItem : UserControl, IListDestinationItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApplicationListDestinationItem" /> class.
        /// </summary>
        /// <param name="registeredApplication">The registered application.</param>
        public ApplicationListDestinationItem(RegisteredApplication registeredApplication)
        {
            InitializeComponent();
            DataContext = this;
            RegisteredApplication = registeredApplication;
        }

        /// <summary>
        ///     Gets or sets the registered application
        /// </summary>
        /// <value>
        ///     The registered application
        /// </value>
        public RegisteredApplication RegisteredApplication { get; set; }

        /// <summary>
        ///     Gets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        ///     Handles the OnMouseDoubleClick event of the ApplicationListDestinationItem control
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data</param>
        private void ApplicationListDestinationItem_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(RegisteredApplication.ApplicationPath);
        }
    }
}