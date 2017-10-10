using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using Missile.TextLauncher.ListPlugin;

namespace Missile.TextLauncher.ApplicationPlugin
{
    /// <summary>
    ///     Interaction logic for ApplicationListDestinationItem.xaml
    /// </summary>
    public partial class ApplicationListDestinationItem : UserControl, IListDestinationItem
    {
        public ApplicationListDestinationItem(RegisteredApplication registeredApplication)
        {
            InitializeComponent();
            DataContext = this;
            RegisteredApplication = registeredApplication;
        }

        public RegisteredApplication RegisteredApplication { get; set; }

        public Guid Id { get; } = Guid.NewGuid();

        private void ApplicationListDestinationItem_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(RegisteredApplication.ApplicationPath);
        }
    }
}