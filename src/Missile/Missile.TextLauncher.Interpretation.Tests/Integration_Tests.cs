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
        public void Handle_Noop_Provider()
        {
            string input = "noop";                                               


            Task task = facade.Execute(input);            
            task.Exception.Should().BeNull();
        }
    }
}
