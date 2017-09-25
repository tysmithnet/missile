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
        public ListDestinationOutput(IObservable<UserControl> items)
        {
            items.Subscribe(control => { UserControls.Add(control); }, exception =>
            {
                ;
            });
            InitializeComponent();
            ItemsListBox.ItemsSource = UserControls;
        }

        public ObservableCollection<UserControl> UserControls { get; set; } = new ObservableCollection<UserControl>();
    }
}