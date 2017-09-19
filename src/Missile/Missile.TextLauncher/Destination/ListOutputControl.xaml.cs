using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Missile.TextLauncher.Destination
{
    /// <summary>
    ///     Interaction logic for ListOutputControl.xaml
    /// </summary>
    public partial class ListOutputControl : UserControl
    {
        public List<ListDestinationItem> Items { get; set; }

        public ListOutputControl(IEnumerable<ListDestinationItem> items)
        {
            Items = items.ToList();
            InitializeComponent();
            ListBox.ItemsSource = Items;
        }
    }
}