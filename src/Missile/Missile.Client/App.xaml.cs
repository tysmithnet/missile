using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using Missile.Core;

namespace Missile.Client
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(location);
            Debug.Assert(path != null,
                "Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) returned null which should not be possible");
            var compositionContainer = new CompositionContainer(new DirectoryCatalog(path));
            var launcher = compositionContainer.GetExportedValue<Launcher>();
            var mainWindow = new MainWindow(launcher);
            mainWindow.Show();
        }
    }
}