using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.IntegrationTests
{
    public class Startup_Should
    {
        [Fact]
        public void Load_In_Under_1_Second()
        {
            using (var client = new MissileClient(1000))
            {
                client.GetMainWindow().Should().NotBeNull("the main window should always show");
            }
        }   
    }
}
