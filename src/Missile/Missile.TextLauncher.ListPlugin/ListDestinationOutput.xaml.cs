using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            items.Subscribe(control => { UserControls.Add(control); }, exception =>
            {
                // todo: do something with errors!
            });
            InitializeComponent();
            ItemsListBox.ItemsSource = UserControls;
        }

        protected internal IDestinationContextMenuProvider[] ContextMenuProviders { get; set; }

        public ObservableCollection<IListDestinationItem> UserControls { get; set; } =
            new ObservableCollection<IListDestinationItem>();

        public void Remove(IListDestinationItem listDestinationItem)
        {
            UserControls.Remove(listDestinationItem);
        }
        
        private void ItemsListBox_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // todo: handle multiple selections
            if (ContextMenu == null)
                ContextMenu = new ContextMenu();
            ContextMenu.Items.Clear();
            foreach (var destinationContextMenuProvider in ContextMenuProviders)
                if (destinationContextMenuProvider.CanHandle(ItemsListBox.SelectedItems.Cast<object>()))
                    foreach (var menuItem in destinationContextMenuProvider.GetMenuItems(ItemsListBox.SelectedItems
                        .Cast<object>()))
                        ContextMenu.Items.Add(menuItem);
            ContextMenu.Placement = PlacementMode.MousePoint;
            ContextMenu.IsOpen = true;
            e.Handled = true;
        }
    }
}