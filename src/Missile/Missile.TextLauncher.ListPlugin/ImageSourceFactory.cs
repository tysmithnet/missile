using System;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace Missile.TextLauncher.ListPlugin
{
    public class ImageSourceFactory
    {
        public static BitmapImage GetBitmapFromResource(Assembly assembly, string path)
        {
            return new BitmapImage(new Uri(@"pack://application:,,,/"
                                           + assembly.GetName().Name
                                           + ";component/"
                                           + path, UriKind.Absolute));
        }
    }
}