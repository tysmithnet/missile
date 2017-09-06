using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Interpretation.Compilation
{
    [Export(typeof(ILexer))]
    public class Lexer : ILexer
    {
        public IEnumerable<Token> Lex(string input)
        {
            if (input == null) throw new NullReferenceException(nameof(input));

            var tokens = new List<Token>();

            input = input.Trim();

            var parts = SplitInputIntoParts(input);

            for (var i = 0; i < parts.Count; i++)
            {
                var part = parts[i];

                if (tokens.LastOrDefault() == null)
                {
                    tokens.Add(new ProviderToken(part));
                    continue;
                }
                if (tokens.LastOrDefault() is OperatorToken operatorToken)
                {
                    switch (operatorToken.Identifier)
                    {
                        case "|":
                            tokens.Add(new FilterToken(part));
                            break;
                        case ">":
                            tokens.Add(new DestinationToken(part));
                            break;
                        default:
                            throw new ApplicationException($"Unexpected operator {operatorToken.Identifier}");
                    }
                    continue;
                }
                if (part == "|" || part == ">")
                {
                    tokens.Add(new OperatorToken(part));
                    continue;
                }
                throw new ApplicationException($"Unable to parse part: {part}");
            }

            return tokens;
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