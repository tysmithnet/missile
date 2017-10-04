using System.Drawing;
using System.Windows.Media;

namespace Missile.TextLauncher.ApplicationPlugin
{
    public class RegisteredApplication
    {
        public ImageSource Icon { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationPath { get; set; }
    }
}