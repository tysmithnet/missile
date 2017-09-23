using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;
using Missile.Core;
using Missile.TextLauncher;
using Missile.TextLauncher.Interpretation;

namespace Missile.Client
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public sealed partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            //var assemblies = LoadPlugins();
            string location = Assembly.GetExecutingAssembly().Location;    
            var compositionContainer = new CompositionContainer(new DirectoryCatalog(Path.GetDirectoryName(location)));
            var launcher = new TextLauncherImplementation();
            compositionContainer.ComposeParts(launcher);
            var mainWindow = new MainWindow(launcher);
            mainWindow.Show();
        }
    }
}