using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using FluentAssertions;
using Missile.TextLauncher.Filtration;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class HeadFilter_Should
    {
        [Fact]
        public void Allow_Only_The_Specified_Number_Of_Items()
        {
            var filter = new HeadFilter();
            filter.Filter("-n 2".Split(), Observable.Range(0, 10).Select(x => x as object)).ToEnumerable().Should()
                .Equal(0, 1);
        }
    }
}