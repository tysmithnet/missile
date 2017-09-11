using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Integration_Tests
    {
        public static InterpretationFacade GetFacade()
        {
            var aggregateCatalog = new AggregateCatalog();
            var facadeAssembly = new AssemblyCatalog(typeof(InterpretationFacade).Assembly);
            var providerAssembly = new AssemblyCatalog(typeof(IProvider<>).Assembly);
            aggregateCatalog.Catalogs.Add(facadeAssembly);
            aggregateCatalog.Catalogs.Add(providerAssembly);
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            var facade = new InterpretationFacade();
            compositionContainer.ComposeParts(facade);
            return facade;
        }

        [Fact]
        public void Handle_List_Output()
        {   
            var input = "noop > list";
            GetFacade().Invoking(f => f.ExecuteAsync(input))
                .ShouldNotThrow("this is the most basic integration test possible");
        }

        [Fact]
        public void Handle_Provider_Filter_Destination()
        {   
            var input = "noop | filter > noop";
            GetFacade().Invoking(f => f.ExecuteAsync(input))
                .ShouldNotThrow("this is the most basic integration test possible");
        }

        [Fact]
        public void Handle_Noop_Provider()
        {                                             
            var input = "noop";
            GetFacade().Invoking(f => f.ExecuteAsync(input))
                .ShouldNotThrow("this is the most basic integration test possible");
        }
    }
}