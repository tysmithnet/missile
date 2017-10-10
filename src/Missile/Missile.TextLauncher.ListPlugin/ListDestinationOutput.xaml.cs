using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    ///     Interaction logic for ListDestinationOutput.xaml
    /// </summary>
    public partial class ListDestinationOutput : UserControl
    {
        public ListDestinationOutput(IObservable<IListDestinationItem> items,
            IDestinationContextMenuProvider[] contextMenuProviders)
        {
            ContextMenuProviders = contextMenuProviders;
            items.Subscribe(listDestinationItem =>
            {
                ListDestinationItems.Add(listDestinationItem);
                if (listDestinationItem is FrameworkElement frameworkElement)
                    frameworkElement.PreviewMouseRightButtonDown += (sender, args) =>
                    {
                        if (!ItemsListBox.SelectedItems.Contains(frameworkElement))
                            ItemsListBox.SelectedItems.Add(frameworkElement);
                    };
            }, exception =>
            {
                // todo: do something with errors!
            });
            InitializeComponent();
            ItemsListBox.ItemsSource = ListDestinationItems;
        }

        protected internal IDestinationContextMenuProvider[] ContextMenuProviders { get; set; }

        public ObservableCollection<IListDestinationItem> ListDestinationItems { get; set; } =
            new ObservableCollection<IListDestinationItem>();

        public void Remove(IListDestinationItem listDestinationItem)
        {
            ListDestinationItems.Remove(listDestinationItem);
        }

        private void ItemsListBox_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ContextMenu == null)
                ContextMenu = new ContextMenu();
            ContextMenu.Items.Clear();
            foreach (var destinationContextMenuProvider in ContextMenuProviders)
            foreach (var menuItem in destinationContextMenuProvider.GetMenuItems(ItemsListBox.SelectedItems
                .OfType<IListDestinationItem>()))
                ContextMenu.Items.Add(menuItem);

            ContextMenu.Placement = PlacementMode.MousePoint;
            ContextMenu.IsOpen = true;
            e.Handled = true;
        }
    }
}