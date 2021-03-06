﻿using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Missile.TextLauncher.Interpretation.Lexing;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    [ExcludeFromCodeCoverage]
    public class Tokens_Should
    {
        [Fact]
        public void Provide_Correct_Equality_Logic()
        {
            var p1 = new ProviderToken("noop", new string[0]);
            var p2 = new ProviderToken("noop", new string[0]);

            var p3 = new ProviderToken("lorem", new[] {"--words", "--count", "--10"});
            var p4 = new ProviderToken("lorem", new[] {"--words", "--count", "--10"});

            p1.Should().Be(p2, "freshly created provider tokens should be equal");
            p1.GetHashCode().Should().Be(p2.GetHashCode(),
                "freshly created provider tokens should have the same hash code");

            p3.Should().Be(p4, "freshly created provider tokens with args should be equal");
            p3.GetHashCode().Should().Be(p4.GetHashCode(),
                "freshly created provider tokens with args should have the same hash code");
        }

        [Fact]
        public void Provide_Correct_Inequality_Logic()
        {
            var p1 = new ProviderToken("noop", new string[0]);
            var p2 = new ProviderToken("lorem", new string[0]);

            var p3 = new ProviderToken("noop", new[] {"--words", "--count", "100"});
            var p4 = new ProviderToken("lorem", new[] {"--words", "--count", "100"});

            p1.Should().NotBe(p2, "freshly created provider tokens should be equal");
            p1.GetHashCode().Should().NotBe(p2.GetHashCode(),
                "freshly created provider tokens should have the same hash code");

            p3.Should().NotBe(p4, "freshly created provider tokens with args should be equal");
            p3.GetHashCode().Should().NotBe(p4.GetHashCode(),
                "freshly created provider tokens with args should have the same hash code");
        }

        [Fact]
        public void Throw_If_No_Input_Provided()
        {
            Action action = () => new ProviderToken(null, null);
            action.ShouldThrow<ArgumentNullException>("tokens cannot have null names");
        }
    }
}