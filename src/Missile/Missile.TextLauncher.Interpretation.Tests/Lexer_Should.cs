using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Lexer_Should
    {
        [Fact]
        public void Handle_Basic_Cases()
        {
            new Lexer().Invoking(l => l.Lex(null)).Should()
                .Be(new Token[0], "null is treated as empty string which is no tokens");

            new Lexer().Invoking(l => l.Lex("")).Should()
                .Be(new Token[0], "empty string implies no tokens");

            new Lexer().Invoking(l => l.Lex("a")).Should()
                .Be(new Token[]
                {
                    new ProviderToken("a", new string[0]), 
                }, "a single letter is treated as a provider with no args");

            new Lexer().Invoking(l => l.Lex("google")).Should()
                .Be(new Token[]
                {
                    new ProviderToken("google", new string[0]),
                }, "a single word is treated as provider with no args");

            new Lexer().Invoking(l => l.Lex("google search")).Should()
                .Be(new Token[]
                {
                    new ProviderToken("google", new string[] {"search"}),
                }, "a single word is treated as provider with no args");
        }
    }
}