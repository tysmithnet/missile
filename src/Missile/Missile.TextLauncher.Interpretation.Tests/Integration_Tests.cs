using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Integration_Tests
    {
        public static InterpretationFacade GetFacade()
        {
            var aggregateCatalog = new AggregateCatalog();
            var types = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("Missile"))
                .SelectMany(x => x.GetExportedTypes());
            aggregateCatalog.Catalogs.Add(new TypeCatalog(types));
            var compositionContainer = new CompositionContainer(aggregateCatalog);
            var facade = new InterpretationFacade();
            compositionContainer.ComposeParts(facade);
            return facade;
        }

        [Fact]
        public void Handle_Conversion()
        {
            var input = "mockobject > mockstring";
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
            var input = "mockobject";
            GetFacade().Invoking(async f => await f.ExecuteAsync(input))
                .ShouldNotThrow("providers can act on their own");
        }

        [Fact]
        public void Handle_Provider_And_Destination()
        {
            var input = "mockobject > mockobject";
            GetFacade().Invoking(async f => await f.ExecuteAsync(input)).ShouldNotThrow("filters are not required");
        }

        [Fact]
        public void Handle_Provider_Filter_Destination()
        {
            var input = "mockobject | mockobject > mockobject";
            GetFacade().Invoking(async f => await f.ExecuteAsync(input))
                .ShouldNotThrow("the most basic full pipeline should pass");
        }
    }
}