﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Tokens_Should
    {
        [Fact]
        public void Provide_Correct_Equality_Logic()
        {
            var p1 = new ProviderToken("noop");
            var p2 = new ProviderToken("noop");

            var p3 = new ProviderToken("lorem --words --count 10");
            var p4 = new ProviderToken("lorem --words --count 10");
            
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
            var p1 = new ProviderToken("noop");
            var p2 = new ProviderToken("lorem");

            var p3 = new ProviderToken("lorem --words --count 100");
            var p4 = new ProviderToken("lorem --words --count 10");

            p1.Should().NotBe(p2, "freshly created provider tokens should be equal");
            p1.GetHashCode().Should().NotBe(p2.GetHashCode(),
                "freshly created provider tokens should have the same hash code");

            p3.Should().NotBe(p4, "freshly created provider tokens with args should be equal");
            p3.GetHashCode().Should().NotBe(p4.GetHashCode(),
                "freshly created provider tokens with args should have the same hash code");
        }}
}