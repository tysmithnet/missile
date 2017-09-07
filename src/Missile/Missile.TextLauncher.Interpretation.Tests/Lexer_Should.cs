using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Missile.TextLauncher.Interpretation.Compilation;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Lexer_Should
    {
        public const string SingleProvider = "shutdown";
        public const string SingleProviderWithArgString = "spotify play --playlist heavy";
        public const string ProviderAndDestination = "todo > list";
        public const string ProviderFilterDestination = "todo | sort > list";
        public const string MultipleFilters = "lorem | sort | unqiue | take 100 > list";

        [Fact]
        public void Correctly_Split_Input()
        {
            Lexer lexer = new Lexer();

            var expectedSingleProvider = new Token[]
            {
                new ProviderToken(SingleProvider),
            };

            var expectedSingleProviderWithArgString = new Token[]
            {
                new ProviderToken(SingleProviderWithArgString), 
            };

            var expectedProviderAndDestination = new Token[]
            {
                new ProviderToken("todo"),
                new DestinationToken("list"), 
            };

            var expectedProviderFilterDestination = new Token[]
            {
                new ProviderToken("todo"),
                new FilterToken("sort"), 
                new DestinationToken("list"),
            };

            var expectedMultipleFilters = new Token[]
            {
                new ProviderToken("lorem"), 
                new FilterToken("sort"), 
                new FilterToken("unique"), 
                new FilterToken("take 100"), 
                new DestinationToken("list"), 
            };

            lexer.Lex(SingleProvider).Should().Equal(expectedSingleProvider, "a single word is treated as a provider with no arg string");
            lexer.Lex(SingleProviderWithArgString).Should().Equal(expectedSingleProviderWithArgString,
                "no pipes or outputs means that everything is provider input");
            lexer.Lex(ProviderAndDestination).Should().Equal(expectedProviderAndDestination, "filters are not required");
            lexer.Lex(ProviderFilterDestination).Should().Equal(expectedProviderFilterDestination,
                "you can specify a provider filter and destination");
            lexer.Lex(MultipleFilters).Should().Equal(expectedMultipleFilters, "you can use multiple filters");
        }
    }
}
