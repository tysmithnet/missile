using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
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
        public void Invoke_Filter_On_The_Instance()
        {
            var filter = new HeadFilter();
            var reg = new RegisteredFilter(filter, typeof(IFilter<object, object>));
            var obs = Observable.Range(0, 10).Select(x => x as object);
            reg.Filter("-n 2".Split(), obs).Should().BeAssignableTo<IObservable<object>>();
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