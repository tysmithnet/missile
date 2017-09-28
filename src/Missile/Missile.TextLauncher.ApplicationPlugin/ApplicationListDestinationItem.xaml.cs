using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Missile.TextLauncher.ApplicationPlugin
{
    /// <summary>
    ///     Interaction logic for ApplicationListDestinationItem.xaml
    /// </summary>
    public partial class ApplicationListDestinationItem : UserControl
    {
        public ApplicationListDestinationItem(ImageSource icon, string applicationName, string applicationPath)
        {
            InitializeComponent();
            Icon = icon;
            ApplicationName = applicationName;
            ApplicationPath = applicationPath;
            IconImage.Source = Icon;
            ApplicationNameTextBlock.Text = ApplicationName;
        }

        public ImageSource Icon { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationPath { get; set; }

        private void ApplicationListDestinationItem_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(ApplicationPath);
        }
    }
}