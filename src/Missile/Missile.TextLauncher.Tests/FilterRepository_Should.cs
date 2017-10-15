using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Missile.TextLauncher.Filtration;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class FilterRepository_Should
    {
        [Fact]
        public void Register_Injected_Filters()
        {
            var first = new FirstFilter();
            var repo = new FilterRepository();
            repo.Filters = new[] {first};
            repo.Get(first.Name).FilterInstance.Should().Be(first);
        }
    }
}