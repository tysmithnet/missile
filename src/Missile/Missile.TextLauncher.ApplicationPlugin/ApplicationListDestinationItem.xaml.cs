using System.Windows.Media.Imaging;
using Missile.TextLauncher.Destination.ListDestination;

namespace Missile.TextLauncher.ApplicationPlugin
{
    /// <summary>
    ///     Interaction logic for ApplicationListDestinationItem.xaml
    /// </summary>
    public partial class ApplicationListDestinationItem : ListDestinationItem
    {
        public ApplicationListDestinationItem(BitmapImage icon, string applicationName, string applicationPath)
        {
            Icon = icon;
            ApplicationName = applicationName;
            ApplicationPath = applicationPath;
            InitializeComponent();
        }

        public BitmapImage Icon { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationPath { get; set; }
    }
}