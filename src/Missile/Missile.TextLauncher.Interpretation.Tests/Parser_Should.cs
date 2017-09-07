using FluentAssertions;
using Missile.TextLauncher.Interpretation.Compilation;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Parser_Should
    {
        [Fact]
        public void Parse_Null_Object_If_Empty()
        {
            var tokens = new Token[] { };
            var parser = new Parser();
            parser.Parse(null).Should().Be(new NullRootNode(), "null is treaded as empty");
            parser.Parse(tokens).Should().Be(new NullRootNode(), "no tokens is treated as empty");
        }

        [Fact]
        public void Parse_Single_Provider()
        {
            var tokens = new Token[]
            {
                new ProviderToken("noop"), 
            };

            Parser parser = new Parser();
            RootNodeBuilder rootNodeBuilder = new RootNodeBuilder()
                .WithProvider("noop");
            parser.Parse(tokens).Should().Be(rootNodeBuilder.Build());
        }
    }
}