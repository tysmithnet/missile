using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(ILexer))]
    public class Lexer : ILexer
    {
        public IEnumerable<Token> Lex(string input)
        {
            return null;
        }

        private List<string> SplitInputIntoParts(string input)
        {
            var startEscape = false;
            var parts = new List<string>();
            var lastIndex = 0;
            for (var i = 0; i < input.Length; i++)
                switch (input[i])
                {
                    case '\\':
                        startEscape = true;
                        break;
                    case '|':
                    case '>':
                        if (!startEscape)
                        {
                            parts.Add(input.Substring(lastIndex, i - lastIndex).Replace(@"\|", "|")
                                .Replace(@"\>", ">"));
                            parts.Add(input.Substring(i, 1));
                            lastIndex = i + 1;
                        }
                        startEscape = false;
                        break;
                    default:
                        startEscape = false;
                        break;
                }
            if (lastIndex < input.Length)
                parts.Add(input.Substring(lastIndex).Replace(@"\|", "|").Replace(@"\>", ">"));

            return parts;
        }
    }
}