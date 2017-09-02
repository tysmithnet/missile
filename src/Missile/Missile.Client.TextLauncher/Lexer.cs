using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Missile.Client.TextLauncher
{
    public class Lexer : ILexer
    {   
        private static readonly Regex IdentifierRegex = new Regex(@"[a-zA-Z0-9_*]");
        private static readonly Regex OperatorRegex = new Regex(@"[|>]");

        public IEnumerable<Token> Lex(string input)
        {
            if(input == null) throw new NullReferenceException(nameof(input));

            List<Token> tokens = new List<Token>();

            input = input.Trim();

            var parts = SplitInputIntoParts(input);
            
            for (int i = 0; i < parts.Count; i++)
            {
                string part = parts[i];

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
            bool startEscape = false;
            List<string> parts = new List<string>();
            int lastIndex = 0;
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '\\':
                        startEscape = true;
                        break;
                    case '|':
                    case '>':
                        if (!startEscape)
                        {
                            parts.Add(input.Substring(lastIndex, i - lastIndex).Replace(@"\|", "|").Replace(@"\>", ">"));
                            parts.Add(input.Substring(i, 1));
                            lastIndex = i + 1;
                        }                                                        
                        startEscape = false;
                        break;
                    default:
                        startEscape = false;
                        break;
                }
            }
            if(lastIndex < input.Length)
                parts.Add(input.Substring(lastIndex).Replace(@"\|", "|").Replace(@"\>", ">"));

            return parts;
        }
    }
}