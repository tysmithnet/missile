using System;
using FluentAssertions;
using Missile.TextLauncher.Interpretation.Compilation;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Parser_Should
    {
        [Fact]
        public void Use_NoopProvider_And_NoopDestination_If_They_Arent_Supplied()
        {
            var tokens = new Token[]
            {

            };

            Parser parser = new Parser();
            RootNode rootNode = new RootNodeBuilder()
                .WithProvider("noop")
                .WithDestination("noop")
                .Build();
            parser.Parse(tokens).Should().Be(rootNode, "nothing is actually required");
        }

        [Fact]
        public void Parse_Respective_Components()
        {
            var tokens = new Token[]
            {
                new ProviderToken("lorem"),
                new FilterToken("sort"),
                new FilterToken("unique"),
                new DestinationToken("console"), 
            };

            Parser parser = new Parser();
            RootNode rootNode = new RootNodeBuilder()
                .WithProvider("lorem")
                .WithFilter("sort")
                .WithFilter("unique")
                .WithDestination("console")
                .Build();
            parser.Parse(tokens).Should().Be(rootNode, "filters are not required");
        }   
    }
}