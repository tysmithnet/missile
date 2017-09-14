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
            var testAssembly = new AssemblyCatalog(typeof(Integration_Tests).Assembly);
            aggregateCatalog.Catalogs.Add(facadeAssembly);
            aggregateCatalog.Catalogs.Add(providerAssembly);
            aggregateCatalog.Catalogs.Add(testAssembly);
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            var facade = new InterpretationFacade();
            compositionContainer.ComposeParts(facade);
            return facade;
        }

        [Fact]
        public void Handle_Conversion()
        {
            var input = "lorem > list";
            GetFacade().Invoking(async f => await f.ExecuteAsync(input))
                .ShouldNotThrow("conversions should be provided if an appropriate converter is registered");
        }

        [Fact]
        public void Handle_No_Input()
        {
            var input = "";
            GetFacade().Invoking(async f => await f.ExecuteAsync(input))
                .ShouldNotThrow("this is the most basic integration test possible");
        }

        [Fact]
        public void Handle_Only_Provider()
        {
            var input = "lorem";
            GetFacade().Invoking(async f => await f.ExecuteAsync(input))
                .ShouldNotThrow("providers can act on their own");
        }

        [Fact]
        public void Handle_Provider_And_Destination()
        {
            var input = "lorem > console";
            GetFacade().Invoking(async f => await f.ExecuteAsync(input)).ShouldNotThrow("filters are not required");
        }

        [Fact]
        public void Handle_Provider_Filter_Destination()
        {
            var input = "lorem | first > console";
            GetFacade().Invoking(async f => await f.ExecuteAsync(input))
                .ShouldNotThrow("the most basic full pipeline should pass");
        }
    }
}