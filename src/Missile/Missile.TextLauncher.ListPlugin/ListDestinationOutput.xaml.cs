using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    ///     Interaction logic for ListDestinationOutput.xaml
    /// </summary>
    public partial class ListDestinationOutput : UserControl
    {
        public ListDestinationOutput(IObservable<IListDestinationItem> items)
        {
            items.Subscribe(control => { UserControls.Add(control); }, exception =>
            {
                // todo: do something with errors!
            });
            InitializeComponent();
            ItemsListBox.ItemsSource = UserControls;
        }

        public ObservableCollection<IListDestinationItem> UserControls { get; set; } =
            new ObservableCollection<IListDestinationItem>();

        public void Remove(IListDestinationItem listDestinationItem)
        {
            UserControls.Remove(listDestinationItem);
        }
    }
}