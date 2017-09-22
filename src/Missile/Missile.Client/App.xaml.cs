using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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
            var assemblies = LoadPlugins();
            var aggregateCatalog = new AggregateCatalog();
            var assemblyCatalog = new AssemblyCatalog(typeof(App).Assembly);
            var coreAssemblyCatalog = new AssemblyCatalog(typeof(Launcher).Assembly);
            var textLauncherAssemblyCatalog = new AssemblyCatalog(typeof(TextLauncherImplementation).Assembly);
            var textLauncherInterpretationAssemblyCatalog = new AssemblyCatalog(typeof(InterpretationFacade).Assembly);
            aggregateCatalog.Catalogs.Add(assemblyCatalog);
            aggregateCatalog.Catalogs.Add(coreAssemblyCatalog);
            aggregateCatalog.Catalogs.Add(textLauncherAssemblyCatalog);
            aggregateCatalog.Catalogs.Add(textLauncherInterpretationAssemblyCatalog);
            foreach (var assembly in assemblies)
                aggregateCatalog.Catalogs.Add(new AssemblyCatalog(assembly));
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            var launcher = new TextLauncherImplementation();
            compositionContainer.ComposeParts(launcher);
            var mainWindow = new MainWindow(launcher);
            mainWindow.Show();
        }

        private static IEnumerable<Assembly> LoadPlugins()
        {
            var xmlReader = new XmlTextReader("plugins.xml");
            var assemblies = new List<Assembly>();
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType != XmlNodeType.Text) continue;
                var assembly = Assembly.LoadFile(xmlReader.Value);
                assemblies.Add(assembly);
            }
            return assemblies;
        }
    }
}