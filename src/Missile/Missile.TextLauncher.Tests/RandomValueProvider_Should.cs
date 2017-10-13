using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using FluentAssertions;
using Missile.TextLauncher.Provision.RandomValue;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
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

        /// <summary>
        ///     Generates the sentences.
        /// </summary>
        [Fact]
        public void Generate_Sentences()
        {
            var sentences = new RandomValueProvider().Provide("lorem -t sentence -c 3 -s 0".Split()).ToEnumerable();
            sentences.Should().Equal("Morbi ultrices euismod tempus.",
                "Maecenas lacus urna, sollicitudin a turpis pellentesque, tempor blandit purus.",
                "Nam sit amet arcu facilisis, cursus libero sit amet, gravida lacus.");
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

        [Fact]
        public void Throw_If_Bad_Parameters_Are_Passed()
        {
            var rng = new RandomValueProvider();
            rng.Invoking(provider => provider.Provide("lorem -t letter".Split())).ShouldThrow<ArgumentException>();
        }
    }
}