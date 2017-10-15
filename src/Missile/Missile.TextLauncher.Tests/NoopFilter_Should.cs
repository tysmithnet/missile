using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using FluentAssertions;
using Missile.TextLauncher.Filtration;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class NoOpFilter_Should
    {
        [Fact]
        public void Not_Do_Anything()
        {
            var filter = new NoOpFilter();
            var obs = Observable.Range(0, 10).Select(x => x as object);
            filter.Filter("".Split(), obs).Should().Be(obs);
        }
    }
}