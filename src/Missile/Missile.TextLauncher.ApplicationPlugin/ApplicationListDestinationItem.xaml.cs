using System;
using System.Collections.Generic;
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
        public ApplicationListDestinationItem(RegisteredApplication registeredApplication,
            IEnumerable<IDestinationContextMenuProvider<RegisteredApplication>> contextMenuProviders)
        {
            InitializeComponent();
            DataContext = this;
            RegisteredApplication = registeredApplication;
            ContextMenu = new ContextMenu();
            foreach (var p in contextMenuProviders)
                if (p.CanHandle(registeredApplication))
                    ContextMenu.Items.Add(p.GetMenuItem(registeredApplication, this));
            MouseRightButtonUp += (sender, args) =>
            {
                if (ContextMenu != null)
                {
                    ContextMenu.PlacementTarget = this;
                    ContextMenu.IsOpen = true;
                }
            };
        }

        public RegisteredApplication RegisteredApplication { get; set; }

        public Guid Id { get; } = Guid.NewGuid();


        private void ApplicationListDestinationItem_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(RegisteredApplication.ApplicationPath);
        }
    }
}