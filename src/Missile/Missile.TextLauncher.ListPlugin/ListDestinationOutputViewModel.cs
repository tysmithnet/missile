using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Missile.TextLauncher.Annotations;

namespace Missile.TextLauncher.ListPlugin
{
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

        public ObservableCollection<FrameworkElement> MenuItems { get; set; } = new ObservableCollection<FrameworkElement>();

        public ObservableCollection<IListDestinationItem> ListDestinationItems { get; set; } =
            new ObservableCollection<IListDestinationItem>();

        protected internal IDestinationContextMenuProvider[] ContextMenuProviders { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PopulateMenuItems(IList<IListDestinationItem> selectedItems)
        {
            var menuItems = ContextMenuProviders.SelectMany(destinationContextMenuProvider =>
                destinationContextMenuProvider.GetMenuItems(selectedItems)).ToList();
            MenuItems.Clear();
            menuItems.ForEach(item => MenuItems.Add(item));
        }
    }
}