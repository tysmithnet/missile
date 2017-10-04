using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Missile.TextLauncher.ApplicationPlugin
{
    /// <summary>
    ///     Interaction logic for ApplicationListDestinationItem.xaml
    /// </summary>
    public partial class ApplicationListDestinationItem : UserControl
    {
        public ApplicationListDestinationItem(RegisteredApplication registeredApplication,
            IEnumerable<IDestinationContextMenuProvider<RegisteredApplication>> contextMenuProviders)
        {
            InitializeComponent();
            Icon = registeredApplication.Icon.ToImageSource();
            ApplicationName = registeredApplication.ApplicationName;
            ApplicationPath = registeredApplication.ApplicationPath;
            IconImage.Source = Icon;
            ApplicationNameTextBlock.Text = ApplicationName;
            ContextMenu = new ContextMenu();
            foreach(var p in contextMenuProviders)
                if (p.CanHandle(registeredApplication))
                    ContextMenu.Items.Add(p.GetMenuItem(registeredApplication));
            MouseRightButtonUp += (sender, args) =>
            {
                if (ContextMenu != null)
                {
                    ContextMenu.PlacementTarget = this;
                    ContextMenu.IsOpen = true;
                }
            };
        }

        public ImageSource Icon { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationPath { get; set; }

        private void ApplicationListDestinationItem_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(ApplicationPath);
        }
    }
}