using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using Missile.TextLauncher;

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
            var compositionContainer = new CompositionContainer(new DirectoryCatalog(Path.GetDirectoryName(location)));
            var launcher = new TextLauncherImplementation();
            compositionContainer.ComposeParts(launcher);
            var mainWindow = new MainWindow(launcher);
            mainWindow.Show();
        }
    }
}