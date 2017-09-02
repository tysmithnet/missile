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
    }
}
