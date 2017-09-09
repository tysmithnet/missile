using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Integration_Tests
    {
        [Fact]
        public void Handle_Noop_Provider()
        {
            AggregateCatalog aggregateCatalog = new AggregateCatalog();
            AssemblyCatalog facadeAssembly = new AssemblyCatalog(typeof(Facade).Assembly);
            AssemblyCatalog providerAssembly = new AssemblyCatalog(typeof(Provider<>).Assembly);
            aggregateCatalog.Catalogs.Add(facadeAssembly);
            aggregateCatalog.Catalogs.Add(providerAssembly);
            CompositionContainer compositionContainer = new CompositionContainer(aggregateCatalog);
            string input = "noop";
            IFacade facade = compositionContainer.GetExportedValue<IFacade>();
            facade.Invoking(f => f.Execute(input)).ShouldNotThrow("this is the most basic integration test possible");
        }
    }
}