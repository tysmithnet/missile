using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Lexer_Should
    {
        [Fact]
        public void Handle_Null_And_Empty_Cases()
        {
            new Lexer().Lex(null).Should()
                .Equal(new Token[0], "null is treated as empty string which is no tokens");

            new Lexer().Lex("").Should()
                .Equal(new Token[0], "empty string implies no tokens");
        }

        [Fact]
        public void Handle_Basic_Cases()
        {                                                      
            new Lexer().Lex("a").Should()
                .Equal(new Token[]
                {
                    new ProviderToken("a", new string[0]), 
                }, "a single letter is treated as a provider with no args");

            new Lexer().Lex("google").Should()
                .Equal(new Token[]
                {
                    new ProviderToken("google", new string[0]),
                }, "a single word is treated as provider with no args");
        }

        [Fact]
        public void Handle_Basic_Args()
        {
            new Lexer().Lex("google search").Should()
                .Equal(new Token[]
                {
                    new ProviderToken("google", new [] {"search"}),
                }, "a single word is treated as provider with no args");
        }
    }
}