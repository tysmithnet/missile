using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Missile.TextLauncher.Provision;
using Missile.TextLauncher.Provision.RandomValue;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class ProviderRepository_Should
    {
        [Fact]
        public void Register_Injected_Providers()
        {
            var repo = new ProviderRepository();
            repo.Providers = new[] {new RandomValueProvider()};
            repo.RegisteredProviders.Count.Should().Be(1);
        }
    }
}