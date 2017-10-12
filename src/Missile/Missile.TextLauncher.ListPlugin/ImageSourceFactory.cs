using System;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    ///     Convenience factory that is capable of getting BitmapImages from embedded resources
    /// </summary>
    public class ImageSourceFactory
    {
        /// <summary>
        ///     Gets the bitmap from resource
        /// </summary>
        /// <param name="assembly">The assembly that contains the resource</param>
        /// <param name="path">The path of the resource</param>
        /// <returns></returns>
        public static BitmapImage GetBitmapFromResource(Assembly assembly, string path)
        {
            return new BitmapImage(new Uri(@"pack://application:,,,/"
                                           + assembly.GetName().Name
                                           + ";component/"
                                           + path, UriKind.Absolute));
        }
    }
}