using System.Windows.Media.Imaging;

namespace Missile.TextLauncher.ApplicationPlugin
{
    public class RegisteredApplication
    {
        public BitmapImage Icon { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationPath { get; set; }
    }
}