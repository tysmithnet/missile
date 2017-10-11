using System.Reactive.Linq;
using FluentAssertions;
using Missile.TextLauncher.Provision.RandomValue;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    public class RandomValueProvider_Should
    {
        [Fact]
        public void Generate_Ints()
        {
            var nums = new RandomValueProvider().Provide("int -m 0 -x 10 -c 10 -s 0".Split()).ToEnumerable();
            nums.Should().Equal(7, 8, 7, 5, 2, 5, 9, 4, 9, 2);
        }

        [Fact]
        public void Generate_Lorem()
        {
            var strings = new RandomValueProvider().Provide("lorem -t word -c 5 -s 0".Split()).ToEnumerable();
            strings.Should().Equal("Etiam", "sollicitudin", "Morbi", "interdum", "sagittis");
        }

        [Fact]
        public void Parse_Options()
        {
            var rng = new RandomValueProvider();
            rng.Invoking(provider => provider.Provide(new[]
            {
                "int",
                "-c",
                "10",
                "-m",
                "0",
                "-x",
                "42"
            })).ShouldNotThrow();
        }
    }
}