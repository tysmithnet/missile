using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Missile.Core;
using Missile.TextLauncher;

namespace Missile.Client
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var aggregateCatalog = new AggregateCatalog();
            var assemblyCatalog = new AssemblyCatalog(typeof(App).Assembly);
            var coreAssemblyCatalog = new AssemblyCatalog(typeof(Launcher).Assembly);
            var textLauncherAssemblyCatalog = new AssemblyCatalog(typeof(TextLauncherImplementation).Assembly);
            aggregateCatalog.Catalogs.Add(assemblyCatalog);
            aggregateCatalog.Catalogs.Add(coreAssemblyCatalog);
            aggregateCatalog.Catalogs.Add(textLauncherAssemblyCatalog);
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            var launcher = compositionContainer.GetExportedValue<Launcher>();
            var mainWindow = new MainWindow(launcher);
            mainWindow.Show();
        }
    }
}