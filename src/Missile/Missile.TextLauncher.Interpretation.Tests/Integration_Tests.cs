using System.ComponentModel.Composition.Hosting;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Integration_Tests
    {
        [Fact]
        public void Handle_Noop_Provider()
        {
            var aggregateCatalog = new AggregateCatalog();
            var facadeAssembly = new AssemblyCatalog(typeof(InterpretationFacade).Assembly);
            var providerAssembly = new AssemblyCatalog(typeof(Provider<>).Assembly);
            aggregateCatalog.Catalogs.Add(facadeAssembly);
            aggregateCatalog.Catalogs.Add(providerAssembly);
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            var input = "noop";
            var facade = compositionContainer.GetExportedValue<IInterpretationFacade>();
            facade.Invoking(f => f.Execute(input)).ShouldNotThrow("this is the most basic integration test possible");
        }
    }
}