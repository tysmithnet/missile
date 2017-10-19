using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    ///     Interaction logic for ListDestinationOutput.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
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

        private void ItemsListBox_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.PopulateMenuItems(ItemsListBox.SelectedItems.OfType<IListDestinationItem>().ToList());
        }
    }
}