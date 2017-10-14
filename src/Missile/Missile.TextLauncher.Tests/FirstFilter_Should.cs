using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using FluentAssertions;
using Missile.TextLauncher.Filtration;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class FirstFilter_Should
    {
        [Fact]
        public void Filter_Only_The_First_Item()
        {
            var first = new FirstFilter();
            first.Filter(Observable.Range(1, 1).Select(x => x as object)).ToEnumerable().Should().Equal(1);
        }
    }
}