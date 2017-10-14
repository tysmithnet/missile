using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Missile.TextLauncher.Provision;
using Missile.TextLauncher.Provision.RandomValue;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
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

        [Fact]
        public void Provide_Correct_Equality_Logic()
        {
            var rng = new RandomValueProvider();
            var reg = new RegisteredProvider(rng, typeof(IProvider<object>));
            var reg2 = new RegisteredProvider(rng, typeof(IProvider<object>));
            reg.Equals(reg2).Should().BeTrue();
            reg.GetHashCode().Equals(reg2.GetHashCode()).Should().BeTrue();
        }
    }
}