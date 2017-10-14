using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Missile.TextLauncher.Filtration;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class Registered_Filter_Should
    {
        [Fact]
        public void Correctly_Set_Properties()
        {
            var first = new FirstFilter();
            var reg = new RegisteredFilter(first, typeof(IFilter<object, object>));
            reg.Name.Equals(first.Name).Should().BeTrue();
            reg.FilterInstance.Should().Be(first);
            reg.FilterMethodInfo.Should().NotBeNull();
        }

        [Fact]
        public void Provide_Correct_Equality_Logic()
        {
            var first = new FirstFilter();
            var reg = new RegisteredFilter(first, typeof(IFilter<object, object>));
            var reg2 = new RegisteredFilter(first, typeof(IFilter<object, object>));
            reg.Equals(reg2).Should().BeTrue();
            reg.GetHashCode().Equals(reg2.GetHashCode()).Should().BeTrue();
        }
    }
}