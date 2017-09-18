using System;
using FluentAssertions;
using Missile.TextLauncher.Interpretation.Lexing;
using Missile.TextLauncher.Interpretation.Parsing;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Parser_Should
    {
        [Fact]
        public void Parse_Respective_Components()
        {
            var tokens = new Token[]
            {
                new ProviderToken("lorem", new string[0]),
                new FilterToken("sort", new string[0]),
                new FilterToken("unique", new string[0]),
                new DestinationToken("console", new string[0])
            };

            var parser = new Parser();
            var rootNode = new RootNodeBuilder()
                .WithProvider("lorem", new string[0])
                .WithFilter("sort", new string[0])
                .WithFilter("unique", new string[0])
                .WithDestination("console", new string[0])
                .Build();
            parser.Parse(tokens).Should().Be(rootNode, "filters are not required");
        }

        [Fact]
        public void Throw_If_Provider_Used_Inappropriately()
        {
            var providerOutOfPlace = new Token[]
            {
                new FilterToken("sort", new string[0]),
                new ProviderToken("lorem", new string[0])
            };

            var filterOutOfPlace = new Token[]
            {
                new ProviderToken("lorem", new string[0]),
                new DestinationToken("console", new string[0]),
                new FilterToken("sort", new string[0])
            };

            var destinationOutOfPlace = new Token[]
            {
                new DestinationToken("console", new string[0]),
                new ProviderToken("lorem", new string[0]),
                new FilterToken("sort", new string[0])
            };

            var parser = new Parser();
            parser.Invoking(p => p.Parse(providerOutOfPlace))
                .ShouldThrow<ArgumentException>("provider cannot come after filter");
            parser.Invoking(p => p.Parse(filterOutOfPlace))
                .ShouldThrow<ArgumentException>("filter cannot come after destination");
            parser.Invoking(p => p.Parse(destinationOutOfPlace))
                .ShouldThrow<ArgumentException>("destination cannot come before provider");
        }

        [Fact]
        public void Use_NoopProvider_And_NoopDestination_If_They_Arent_Supplied()
        {
            var tokens = new Token[]
            {
            };

            var parser = new Parser();
            var rootNode = new RootNodeBuilder()
                .WithProvider("noop", new string[0])
                .WithDestination("noop", new string[0])
                .Build();
            parser.Parse(tokens).Should().Be(rootNode, "nothing is actually required");
        }
    }
}