using FluentAssertions;
using Missile.TextLauncher.Interpretation.Parsing;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class RootNode_Should
    {
        [Fact]
        public void Provide_Correct_Equality_Logic()
        {
            var r1 = new RootNode();
            var r2 = new RootNode();

            var r3 = new RootNodeBuilder()
                .WithProvider("noop", new string[0])
                .Build();
            var r4 = new RootNodeBuilder()
                .WithProvider("noop", new string[0])
                .Build();

            var r5 = new RootNodeBuilder()
                .WithFilter("filter", new string[0])
                .Build();
            var r6 = new RootNodeBuilder()
                .WithFilter("filter", new string[0])
                .Build();

            var r7 = new RootNodeBuilder()
                .WithDestination("destination", new string[0])
                .Build();
            var r8 = new RootNodeBuilder()
                .WithDestination("destination", new string[0])
                .Build();

            var r9 = new RootNodeBuilder()
                .WithProvider("provider", new string[0])
                .WithFilter("sort", new string[0])
                .WithDestination("list", new string[0])
                .Build();
            var r10 = new RootNodeBuilder()
                .WithProvider("provider", new string[0])
                .WithFilter("sort", new string[0])
                .WithDestination("list", new string[0])
                .Build();

            r1.Should().Be(r2, "freshly created root nodes are equal");
            r1.GetHashCode().Should().Be(r2.GetHashCode(), "freshly created root nodes are equal");

            r3.Should().Be(r4, "if only providers are set, and they are equal then root nodes are equal");
            r3.GetHashCode().Should().Be(r4.GetHashCode(),
                "if only providers are set, and they are equal then root nodes are equal");

            r5.Should().Be(r6, "if only filters are set, and they are equal then root nodes are equal");
            r5.GetHashCode().Should().Be(r6.GetHashCode(),
                "if only filters are set, and they are equal then root nodes are equal");

            r7.Should().Be(r8, "if only destinations are set, and they are equal then root nodes are equal");
            r7.GetHashCode().Should().Be(r8.GetHashCode(),
                "if only destinations are set, and they are equal then root nodes are equal");

            r9.Should().Be(r10, "if all components match then the root nodes are equal");
            r9.GetHashCode().Should().Be(r10.GetHashCode(), "if all components match then the root nodes are equal");

            new RootNode().Equals(new object()).Should().BeFalse("RootNode can only equal other root nodes");
        }

        [Fact]
        public void Provide_Correct_Inequality_Logic()
        {
            var r3 = new RootNodeBuilder()
                .WithProvider("noop", new string[0])
                .Build();
            var r4 = new RootNodeBuilder()
                .WithProvider("lorem", new string[0])
                .Build();

            var r5 = new RootNodeBuilder()
                .WithFilter("filter", new string[0])
                .Build();
            var r6 = new RootNodeBuilder()
                .WithFilter("sort", new string[0])
                .Build();

            var r7 = new RootNodeBuilder()
                .WithDestination("destination", new string[0])
                .Build();
            var r8 = new RootNodeBuilder()
                .WithDestination("list", new string[0])
                .Build();

            var r9 = new RootNodeBuilder()
                .WithProvider("provider", new string[0])
                .WithFilter("filter", new string[0])
                .WithDestination("list", new string[0])
                .Build();
            var r10 = new RootNodeBuilder()
                .WithProvider("provider", new string[0])
                .WithFilter("sort", new string[0])
                .WithDestination("list", new string[0])
                .Build();

            r3.Should().NotBe(r4, "if only providers are set, and they are equal then root nodes are equal");
            r3.GetHashCode().Should().NotBe(r4.GetHashCode(),
                "if only providers are set, and they are equal then root nodes are equal");

            r5.Should().NotBe(r6, "if only filters are set, and they are equal then root nodes are equal");
            r5.GetHashCode().Should().NotBe(r6.GetHashCode(),
                "if only filters are set, and they are equal then root nodes are equal");

            r7.Should().NotBe(r8, "if only destinations are set, and they are equal then root nodes are equal");
            r7.GetHashCode().Should().NotBe(r8.GetHashCode(),
                "if only destinations are set, and they are equal then root nodes are equal");

            r9.Should().NotBe(r10, "if all components match then the root nodes are equal");
            r9.GetHashCode().Should().NotBe(r10.GetHashCode(), "if all components match then the root nodes are equal");
        }
    }
}