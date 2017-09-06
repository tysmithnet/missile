using FluentAssertions;
using Missile.TextLauncher.Interpretation.Compilation;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Parser_Should
    {
        [Fact]
        public void Return_Null_Object_If_Empty_Input()
        {
            var tokens = new Token[] { };
            var parser = new Parser();
            parser.Parse(null).Should().Be(new NullRootNode(), "null is treaded as empty");
            parser.Parse(tokens).Should().Be(new NullRootNode(), "no tokens is treated as empty");
        }
    }
}