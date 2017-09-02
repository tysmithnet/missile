using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Missile.Client.TextLauncher
{
    public class Lexer : ILexer
    {   
        private static readonly Regex IdentifierRegex = new Regex(@"[a-zA-Z9-0_*]");

        public IEnumerable<IToken> Lex(string input)
        {
            if(input == null) throw new NullReferenceException(nameof(input));

            List<IToken> tokens = new List<IToken>();

            input = input.Trim();

            int startOfFilters = ExtractProvider(input);

            return tokens;
        }

        private int ExtractProvider(string input)
        {
            
        }
    }
}