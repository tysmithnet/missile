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
                }, "a two single words are interpretted as provider and one arg");

            new Lexer().Lex("google search long cat").Should()
                .Equal(new Token[]
                {
                    new ProviderToken("google", new [] {"search", "long", "cat"}),
                }, "n words are treated as a provider followed by n-1 args");

            new Lexer().Lex("everything -type image --regex *.cs").Should()
                .Equal(new Token[]
                {
                    new ProviderToken("everything", new [] {"-type", "image", "--regex", "*.cs"}),
                }, "n words are treated as a provider followed by n-1 args and hyphens are included");
        }

        [Fact]
        public void Handle_Basic_Filters()
        {
            new Lexer().Lex("lorem | sort").Should()
                .Equal(new Token[]
                {
                    new ProviderToken("lorem", new string[0]),
                    new OperatorToken("|", new string[0]), 
                    new FilterToken("sort", new string[0]), 
                }, "two words separated by | is interpretted as provider and filter");

            new Lexer().Lex("lorem | sort | first").Should()
                .Equal(new Token[]
                {
                    new ProviderToken("lorem", new string[0]),
                    new OperatorToken("|", new string[0]),
                    new FilterToken("sort", new string[0]),
                    new OperatorToken("|", new string[0]),
                    new FilterToken("first", new string[0]), 
                }, "multiple words separated by | is interpretted as a provider followed by multiple filters");
        }

        [Fact]
        public void Handle_Filters_With_Args()
        {
            new Lexer().Lex("lorem | sort --prop Length").Should().Equal(new Token[]
            {
                new ProviderToken("lorem", new string[0]),
                new OperatorToken("|", new string[0]),
                new FilterToken("sort", new[] {"--prop", "Length"}),
            }, "filters can have args");

            new Lexer().Lex("lorem | sort --prop Length | first ").Should().Equal(new Token[]
            {
                new ProviderToken("lorem", new string[0]),
                new OperatorToken("|", new string[0]),
                new FilterToken("sort", new[] {"--prop", "Length"}),
                new OperatorToken("|", new string[0]), 
                new FilterToken("first", new string[0]), 
            }, "filters can have args");
        }
    }
}