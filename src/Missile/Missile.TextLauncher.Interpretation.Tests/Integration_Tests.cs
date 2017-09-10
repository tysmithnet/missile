using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Integration_Tests
    {
        [Fact]
        public void Handle_List_Output()
        {
            var aggregateCatalog = new AggregateCatalog();
            var facadeAssembly = new AssemblyCatalog(typeof(InterpretationFacade).Assembly);
            var providerAssembly = new AssemblyCatalog(typeof(IProvider<>).Assembly);
            aggregateCatalog.Catalogs.Add(facadeAssembly);
            aggregateCatalog.Catalogs.Add(providerAssembly);
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            var facade = new InterpretationFacade();
            compositionContainer.ComposeParts(facade);
            var input = "noop > list";
            facade.Invoking(f => f.ExecuteAsync(input))
                .ShouldNotThrow("this is the most basic integration test possible");
        }

        [Fact]
        public void Handle_Noop_Provider()
        {
            var aggregateCatalog = new AggregateCatalog();
            var facadeAssembly = new AssemblyCatalog(typeof(InterpretationFacade).Assembly);
            var providerAssembly = new AssemblyCatalog(typeof(IProvider<>).Assembly);
            aggregateCatalog.Catalogs.Add(facadeAssembly);
            aggregateCatalog.Catalogs.Add(providerAssembly);
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            var facade = new InterpretationFacade();
            compositionContainer.ComposeParts(facade);
            var input = "noop";
            facade.Invoking(f => f.ExecuteAsync(input))
                .ShouldNotThrow("this is the most basic integration test possible");
        }
    }
}