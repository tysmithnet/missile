using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Missile.TextLauncher.Annotations;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    ///     Interaction logic for ListDestinationOutput.xaml
    /// </summary>
    public partial class ListDestinationOutput : UserControl
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ListDestinationOutput" /> class
        /// </summary>
        /// <param name="items">The items</param>
        /// <param name="contextMenuProviders">The context menu providers</param>
        public ListDestinationOutput(IObservable<IListDestinationItem> items,
            IDestinationContextMenuProvider[] contextMenuProviders)
        {
            Source = items;
            ContextMenuProviders = contextMenuProviders;       
            ViewModel = new ListDestinationOutputViewModel(items, contextMenuProviders);
            DataContext = ViewModel;
            InitializeComponent();  
        }

        public ListDestinationOutputViewModel ViewModel { get; set; }

        protected internal IObservable<IListDestinationItem> Source { get; set; }

        /// <summary>
        ///     Gets or sets the context menu providers
        /// </summary>
        /// <value>
        ///     The context menu providers
        /// </value>
        protected internal IDestinationContextMenuProvider[] ContextMenuProviders { get; set; }

        /// <summary>
        ///     Gets or sets the list destination items
        /// </summary>
        /// <value>
        ///     The list destination items
        /// </value>
        public ObservableCollection<IListDestinationItem> ListDestinationItems { get; set; } =
            new ObservableCollection<IListDestinationItem>();

        /// <summary>
        ///     Removes the specified list destination item
        /// </summary>
        /// <param name="listDestinationItem">The list destination item</param>
        public void Remove(IListDestinationItem listDestinationItem)
        {
            ListDestinationItems.Remove(listDestinationItem);
        }

        /// <summary>
        ///     Handles the OnPreviewMouseRightButtonDown event of the ItemsListBox control
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs" /> instance containing the event data.</param>
        private void ItemsListBox_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.Assert(ContextMenu != null, "ConextMenu for ListDestinationOutput should not be null");
            ContextMenu.Placement = PlacementMode.MousePoint;
            ContextMenu.IsOpen = true;
            e.Handled = true;
        }
    }

    public class ListDestinationOutputViewModel : INotifyPropertyChanged
    {
        public ListDestinationOutputViewModel(IObservable<IListDestinationItem> items,
            IDestinationContextMenuProvider[] contextMenuProviders)
        {
            ContextMenuProviders =
                contextMenuProviders ?? throw new ArgumentNullException(nameof(contextMenuProviders));
            items.Subscribe(listDestinationItem =>
            {
                ListDestinationItems.Add(listDestinationItem);
            }, exception =>
            {
                // todo: do something with errors!
            });
        }

        public ObservableCollection<MenuItem> MenuItems { get; set; }

        public ObservableCollection<IListDestinationItem> ListDestinationItems { get; set; } =
            new ObservableCollection<IListDestinationItem>();

        protected internal IDestinationContextMenuProvider[] ContextMenuProviders { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable<MenuItem> GetMenuItems(IList<IListDestinationItem> selectedItems)
        {
            return ContextMenuProviders.SelectMany(destinationContextMenuProvider =>
                destinationContextMenuProvider.GetMenuItems(selectedItems));
        }
    }
}