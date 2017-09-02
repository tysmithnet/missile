using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Missile.Client.TextLauncher.Tests
{
    public class Lexer_Should
    {
        [Fact]
        public void Throw_If_Input_Is_Null()
        {   
            Lexer lexer = new Lexer();
            Assert.Throws<NullReferenceException>(() => lexer.Lex(null));
        }

        [Fact]
        public void Return_Empty_If_Empty_String()
        {
            Lexer lexer = new Lexer();
            Assert.Equal(new Token[]{}, lexer.Lex(""));
        }

        [Fact]
        public void Parse_A_Single_Provider_With_No_ArgString()
        {
            string input = @"google";
            Lexer lexer = new Lexer();
            var tokens = lexer.Lex(input);

            var expectedResult = new Token[]
            {
                new ProviderToken
                {
                    Identifier = "google",
                    ArgString = ""
                }, 
            };

            tokens.Should().Equal(expectedResult, "a single word as input should be interpretted as the provider");
        }

        [Fact]
        public void Parse_A_Single_Provider_With_ArgString()
        {
            string input = @"google search long cat";
            Lexer lexer = new Lexer();
            var tokens = lexer.Lex(input);

            var expectedResult = new Token[]
            {
                new ProviderToken
                {
                    Identifier = "google",
                    ArgString = "search long cat"
                },
            };

            tokens.Should().Equal(expectedResult, "input with no operators is a provider followed by an argstring");
        }

        [Fact]
        public void Include_Pipe_Character_If_Escaped()
        {
            string input = @"google search \| unicode";
            
            Lexer lexer = new Lexer();
            var tokens = lexer.Lex(input);

            var expected = new Token[]
            {
                new ProviderToken
                {
                    Identifier = "google",
                    ArgString = "search | unicode"
                }, 
            };

            tokens.Should().Equal(expected, "an escaped pipe should appear in the arg string unescaped");
        }

        [Fact]
        public void Recognize_Difference_Between_Escaped_And_Pipe_Operator()
        {
            string input = @"google search \| unicode | sort";

            Lexer lexer = new Lexer();
            var tokens = lexer.Lex(input);

            var expected = new Token[]
            {
                new ProviderToken
                {
                    Identifier = "google",
                    ArgString = "search | unicode "
                },
                new OperatorToken
                {
                    Identifier = "|",
                    ArgString = ""
                }, 
                new FilterToken
                {
                    Identifier = "sort",
                    ArgString = ""
                }, 
            };

            tokens.Should().Equal(expected, "an escaped pipe should appear in the arg string unescaped, but should still recognize a pipe operator");
        }

        [Fact]
        public void Include_Output_Operator_If_Escaped()
        {
            string input = @"google search \> unicode";

            Lexer lexer = new Lexer();
            var tokens = lexer.Lex(input);

            var expected = new Token[]
            {
                new ProviderToken
                {
                    Identifier = "google",
                    ArgString = "search > unicode"
                },
            };

            tokens.Should().Equal(expected, "an escaped output operator should appear in the arg string unescaped");
        }

        [Fact]
        public void Recognize_Difference_Between_Escaped_And_Output_Operator()
        {
            string input = @"google search \> unicode > list";

            Lexer lexer = new Lexer();
            var tokens = lexer.Lex(input);

            var expected = new Token[]
            {
                new ProviderToken
                {
                    Identifier = "google",
                    ArgString = "search > unicode "
                },
                new OperatorToken
                {
                    Identifier = ">",
                    ArgString = ""
                },
                new FilterToken
                {
                    Identifier = "list",
                    ArgString = ""
                },
            };

            tokens.Should().Equal(expected, "an escaped output operator should appear in the arg string unescaped, but should still recognize a output operator");
        }
    }
}
