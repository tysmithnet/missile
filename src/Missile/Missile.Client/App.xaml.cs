using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Autofac.Core;

namespace Missile.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer container;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var containerBuilder = new ContainerBuilder();

            var pluginAssemblies = GetPluginFilePaths().Select(Assembly.LoadFile).ToArray();

            containerBuilder.RegisterAssemblyTypes(typeof(App).Assembly)
                .AsImplementedInterfaces();

            containerBuilder.RegisterType(typeof(MainWindow));

            containerBuilder.RegisterAssemblyTypes(pluginAssemblies)
                .AsImplementedInterfaces();

            containerBuilder.RegisterAssemblyTypes(pluginAssemblies)
                .Where(t => t.IsAssignableTo<Launcher>())
                .As<Launcher>();

            container = containerBuilder.Build();

            MainWindow = container.Resolve<MainWindow>();
            MainWindow.Show();
        }

        private string[] GetPluginFilePaths()
        {
            return new[]
            {
                @"C:\Users\master\Documents\computing\projects\missile\src\Missile\Missile.Client.TextLauncher\bin\Debug\Missile.Client.TextLauncher.dll"
            };
        }
    }
}
