using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using FluentAssertions;
using Missile.TextLauncher.Filtration;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    [ExcludeFromCodeCoverage]
    public class DistinctFilter_Should
    {
        [Fact]
        public void Only_Pass_On_Distinct_Items()
        {
            var filter = new DistinctFilter();
            filter.Filter("".Split(), Observable.Repeat(1, 10).Select(x => x as object)).ToEnumerable().Should()
                .Equal(1);
        }
    }
}