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
        public ListDestinationOutput(IObservable<FrameworkElement> items)
        {
            items.Subscribe(control =>
            {     
                UserControls.Add(control);
            }, exception =>
            {
                // todo: do something with errors!
            });
            InitializeComponent();
            ItemsListBox.ItemsSource = UserControls;
        }

        public ObservableCollection<FrameworkElement> UserControls { get; set; } = new ObservableCollection<FrameworkElement>();
    }
}