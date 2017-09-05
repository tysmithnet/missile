using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Missile.TextLauncher.Interpretation.Tests.Mocks;
using Moq;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Integration_Tests
    {
        [Fact]
        public void Handle_Only_Provider_No_Args()
        {
            AssemblyCatalog assemblyCatalog = new AssemblyCatalog(typeof(IFacade).Assembly);
            TypeCatalog typeCatalog = new TypeCatalog(typeof(NoopProvider));
            AggregateCatalog aggregateCatalog = new AggregateCatalog(assemblyCatalog, typeCatalog);
            CompositionContainer compositionContainer = new CompositionContainer(aggregateCatalog);
           
            string input = "noop";
            IFacade facade = compositionContainer.GetExport<IFacade>().Value;
            Task task = facade.Execute(input);            
            task.Exception.Should().BeNull();
        }
    }
}
