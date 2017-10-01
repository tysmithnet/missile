using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    ///     Interaction logic for ListDestinationOutput.xaml
    /// </summary>
    public partial class ListDestinationOutput : UserControl
    {
        public ListDestinationOutput(IObservable<UIElement> items)
        {
            items.Subscribe(control => { UserControls.Add(control); }, exception =>
            {
                ;
            });
            InitializeComponent();
            ItemsListBox.ItemsSource = UserControls;
        }

        public ObservableCollection<UIElement> UserControls { get; set; } = new ObservableCollection<UIElement>();
    }
}