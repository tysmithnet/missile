using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Missile.Client.TextLauncher.Tests
{
    public class Parser_Should
    {
        [Fact]
        public void Throw_If_Provider_Is_Not_First_Token()
        {
            var tokens = new Token[]
            {
                new FilterToken("sort -name"), 
            };
            var parser = new Parser();
            parser.Invoking(p => p.Parse(tokens)).ShouldThrow<ArgumentException>("ProviderToken should be the first token");
        }

        [Fact]
        public void Throw_If_DestinationToken_Isnt_Last()
        {
            var tokens = new Token[]
            {
                new ProviderToken("google search long cat"), 
                new FilterToken("sort -name"), 
            };

            var parser = new Parser();

            parser.Invoking(p => p.Parse(tokens)).ShouldThrow<ArgumentException>("DestinationToken has to be last");
        }

        [Fact]
        public void Throw_If_More_Than_One_DestinationToken()
        {
            var tokens = new Token[]
            {
                new DestinationToken("list vertical"), 
                new DestinationToken("grid small"), 
            };

            var parser = new Parser();

            parser.Invoking(p => p.Parse(tokens)).ShouldThrow<ArgumentException>("Cannot have more than 1 DestinationToken");
        }

        [Fact]
        public void Throw_If_More_Than_One_ProviderToken()
        {
            var tokens = new Token[]
            {
                new ProviderToken("google search"),
                new ProviderToken("espn scores"), 
            };

            var parser = new Parser();

            parser.Invoking(p => p.Parse(tokens)).ShouldThrow<ArgumentException>("Cannot have more than 1 ProviderToken");
        }

        [Fact]
        public void Throw_If_Multiple_Output_Operators()
        {
            var tokens = new Token[]
            {
                new ProviderToken("google search"),
                new OperatorToken("|"),
                new FilterToken("count -name"),
                new OperatorToken(">"),
                new FilterToken("sort"),
                new OperatorToken(">"),
                new DestinationToken("list small"),
            };

            var parser = new Parser();

            parser.Invoking(p => p.Parse(tokens))
                .ShouldThrow<ArgumentException>("you cannot have multiple output operators");
            
        }

        [Fact]
        public void Parse_Multiple_Filters()
        {
            var tokens = new Token[]
            {
                new ProviderToken("google search"),
                new OperatorToken("|"), 
                new FilterToken("count -name"), 
                new OperatorToken("|"), 
                new FilterToken("sort"), 
                new OperatorToken(">"), 
                new DestinationToken("list small"), 
            };

            var parser = new Parser();
            var expected = new RootNode();
            expected.ProviderNode = new ProviderNode(tokens[0] as ProviderToken);
            expected.FilterNodes = tokens.OfType<FilterToken>().Select(x => new FilterNode(x)).ToList();
            expected.DestinationNode = new DestinationNode(tokens.Last() as DestinationToken);

            var result = parser.Parse(tokens);
            result.Should().Be(expected, "Parser should recognize multiple filters");
        }
    }
}
