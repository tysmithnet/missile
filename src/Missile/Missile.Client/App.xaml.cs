using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
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
            var aggregateCatalog = new AggregateCatalog();
            var assemblyCatalog = new AssemblyCatalog(typeof(App).Assembly);
            var coreAssemblyCatalog = new AssemblyCatalog(typeof(Launcher).Assembly);
            var textLauncherAssemblyCatalog = new AssemblyCatalog(typeof(TextLauncherImplementation).Assembly);
            var textLauncherInterpretationAssemblyCatalog = new AssemblyCatalog(typeof(InterpretationFacade).Assembly);
            aggregateCatalog.Catalogs.Add(assemblyCatalog);
            aggregateCatalog.Catalogs.Add(coreAssemblyCatalog);
            aggregateCatalog.Catalogs.Add(textLauncherAssemblyCatalog);
            aggregateCatalog.Catalogs.Add(textLauncherInterpretationAssemblyCatalog);
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            var launcher = new TextLauncherImplementation();
            compositionContainer.ComposeParts(launcher);
            var mainWindow = new MainWindow(launcher);
            mainWindow.Show();
        }
    }
}