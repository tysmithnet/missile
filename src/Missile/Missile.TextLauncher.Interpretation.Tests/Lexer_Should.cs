using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using FluentAssertions;
using Missile.TextLauncher.Interpretation.Lexing;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    [ExcludeFromCodeCoverage]
    public class Lexer_Should
    {
        [Fact]
        public void Handle_All_Primary_Components()
        {
            new Lexer().LexAsync("lorem | sort > list", CancellationToken.None).Result.Should().Equal(new Token[]
            {
                new ProviderToken("lorem", new string[0]),
                new OperatorToken("|", new string[0]),
                new FilterToken("sort", new string[0]),
                new OperatorToken(">", new string[0]),
                new DestinationToken("list", new string[0])
            }, "three words separated by | and > are provider, filter, and destination");

            new Lexer().LexAsync("lorem --words 10 | sort --Length > list --head 5", CancellationToken.None).Result
                .Should().Equal(new Token[]
                {
                    new ProviderToken("lorem", new[] {"--words", "10"}),
                    new OperatorToken("|", new string[0]),
                    new FilterToken("sort", new[] {"--Length"}),
                    new OperatorToken(">", new string[0]),
                    new DestinationToken("list", new[] {"--head", "5"})
                }, "providers, filters, and destinations can have arguments");
        }

        [Fact]
        public void Handle_Basic_Args()
        {
            new Lexer().LexAsync("google search", CancellationToken.None).Result.Should()
                .Equal(new Token[]
                {
                    new ProviderToken("google", new[] {"search"})
                }, "a two single words are interpretted as provider and one arg");

            new Lexer().LexAsync("google search long cat", CancellationToken.None).Result.Should()
                .Equal(new Token[]
                {
                    new ProviderToken("google", new[] {"search", "long", "cat"})
                }, "n words are treated as a provider followed by n-1 args");

            new Lexer().LexAsync("everything -type image --regex *.cs", CancellationToken.None).Result.Should()
                .Equal(new Token[]
                {
                    new ProviderToken("everything", new[] {"-type", "image", "--regex", "*.cs"})
                }, "n words are treated as a provider followed by n-1 args and hyphens are included");
        }

        [Fact]
        public void Handle_Basic_Cases()
        {
            new Lexer().LexAsync("a", CancellationToken.None).Result.Should()
                .Equal(new Token[]
                {
                    new ProviderToken("a", new string[0])
                }, "a single letter is treated as a provider with no args");

            new Lexer().LexAsync("  a", CancellationToken.None).Result.Should()
                .Equal(new Token[]
                {
                    new ProviderToken("a", new string[0])
                }, "there can be space before the start of the provider");

            new Lexer().LexAsync("google", CancellationToken.None).Result.Should()
                .Equal(new Token[]
                {
                    new ProviderToken("google", new string[0])
                }, "a single word is treated as provider with no args");
        }

        [Fact]
        public void Handle_Basic_Destinations()
        {
            new Lexer().LexAsync("lorem > list", CancellationToken.None).Result.Should().Equal(new Token[]
            {
                new ProviderToken("lorem", new string[0]),
                new OperatorToken(">", new string[0]),
                new DestinationToken("list", new string[0])
            }, "two words separated by > should be treated as a provider and a destination");

            new Lexer().LexAsync("lorem > list --head 10", CancellationToken.None).Result.Should().Equal(new Token[]
            {
                new ProviderToken("lorem", new string[0]),
                new OperatorToken(">", new string[0]),
                new DestinationToken("list", new[] {"--head", "10"})
            }, "destinations should be able to take arguments");
        }

        [Fact]
        public void Handle_Basic_Filters()
        {
            new Lexer().LexAsync("lorem | sort", CancellationToken.None).Result.Should()
                .Equal(new Token[]
                {
                    new ProviderToken("lorem", new string[0]),
                    new OperatorToken("|", new string[0]),
                    new FilterToken("sort", new string[0])
                }, "two words separated by | is interpretted as provider and filter");

            new Lexer().LexAsync("lorem | sort | first", CancellationToken.None).Result.Should()
                .Equal(new Token[]
                {
                    new ProviderToken("lorem", new string[0]),
                    new OperatorToken("|", new string[0]),
                    new FilterToken("sort", new string[0]),
                    new OperatorToken("|", new string[0]),
                    new FilterToken("first", new string[0])
                }, "multiple words separated by | is interpretted as a provider followed by multiple filters");
        }

        [Fact]
        public void Handle_Double_Quotes_In_Args()
        {
            new Lexer().LexAsync(@"echo ""three   spaces""", CancellationToken.None).Result.Should().Equal(new Token[]
            {
                new ProviderToken("echo", new[] {"three   spaces"})
            }, "double quotes indicates a literal string");

            new Lexer().LexAsync(@"echo double quote: \""", CancellationToken.None).Result.Should().Equal(new Token[]
            {
                new ProviderToken("echo", new[] {"double", "quote:", "\""})
            }, "escaped quotes should be treated as regular characters");

            new Lexer().LexAsync(@"echo ""quote \"" in quote""", CancellationToken.None).Result.Should().Equal(
                new Token[]
                {
                    new ProviderToken("echo", new[] {"quote \" in quote"})
                }, "escaped double quote in quotes appear in the string as a single character");
        }

        /// <summary>
        ///     Handles the escaped characters.
        /// </summary>
        [Fact]
        public void Handle_Escaped_Characters()
        {
            new Lexer().LexAsync(@"noop \t", CancellationToken.None).Result.Should().Equal(new Token[]
            {
                new ProviderToken("noop", new[] {"\t"})
            }, "because \\t should be treated as a tab character");

            new Lexer().LexAsync(@"noop \\t", CancellationToken.None).Result.Should().Equal(new Token[]
            {
                new ProviderToken("noop", new[] {@"\t"})
            }, "double back slash means a literal back slash should be used as input");

            new Lexer().LexAsync(@"noop \\t", CancellationToken.None).Result.Should()
                .Equal(new ProviderToken("noop", new[] {@"\t"}));

            new Lexer().LexAsync(@"noop \t\n\r", CancellationToken.None).Result.Should()
                .Equal(new ProviderToken("noop", new[] {"\t\n\r"}));
        }

        [Fact]
        public void Handle_Filters_With_Args()
        {
            new Lexer().LexAsync("lorem | sort --prop Length", CancellationToken.None).Result.Should().Equal(new Token[]
            {
                new ProviderToken("lorem", new string[0]),
                new OperatorToken("|", new string[0]),
                new FilterToken("sort", new[] {"--prop", "Length"})
            }, "filters can have args");

            new Lexer().LexAsync("lorem | sort --prop Length | first ", CancellationToken.None).Result.Should().Equal(
                new Token[]
                {
                    new ProviderToken("lorem", new string[0]),
                    new OperatorToken("|", new string[0]),
                    new FilterToken("sort", new[] {"--prop", "Length"}),
                    new OperatorToken("|", new string[0]),
                    new FilterToken("first", new string[0])
                }, "filters can have args");
        }

        [Fact]
        public void Handle_Null_And_Empty_Cases()
        {
            new Lexer().LexAsync(null, CancellationToken.None).Result.Should()
                .Equal(new Token[0], "null is treated as empty string which is no tokens");

            new Lexer().LexAsync("", CancellationToken.None).Result.Should()
                .Equal(new Token[0], "empty string implies no tokens");
        }

        [Fact]
        public void Throw_If_Unrecognized_Espcape_Sequence()
        {
            var lexer = new Lexer();
            Func<object> f = () => lexer.LexAsync(@"noop \x", CancellationToken.None).Result;
            f.Invoking(func => func()).ShouldThrow<FormatException>();
        }

        [Fact]
        public void Transition_To_ErrorState_If_Illegal_Character_Encountered()
        {
            new Lexer().Invoking(lexer =>
                {
                    var x = lexer.LexAsync(">", CancellationToken.None).Result;
                })
                .ShouldThrow<InvalidOperationException>("> is not a valid character to start the identifier");

            new Lexer().Invoking(lexer =>
                {
                    var x = lexer.LexAsync("abc$ad", CancellationToken.None).Result;
                })
                .ShouldThrow<InvalidOperationException>("$ is not a valid character to be in an identifier");
        }
    }
}