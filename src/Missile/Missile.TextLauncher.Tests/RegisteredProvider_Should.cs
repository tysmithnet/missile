using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Missile.TextLauncher.Provision;
using Missile.TextLauncher.Provision.RandomValue;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    public class RegisteredProvider_Should
    {
        [Fact]
        public void Correctly_Set_Values()
        {
            var rng = new RandomValueProvider();
            var reg = new RegisteredProvider(rng, typeof(IProvider<object>));
            reg.Name.Equals(rng.Name).Should().BeTrue();
            reg.ProvideMethodInfo.Should().NotBeNull();
            reg.ProviderInstance.Should().Be(rng);
        }
    }
}
