﻿using System;
using System.Collections.Generic;

namespace Missile.Client.TextLauncher.Compilation
{
    public abstract class Token
    {
        public string Identifier { get; protected internal set; }
        public string ArgString { get; protected internal set; }

        internal Token()
        {

        }

        protected Token(string input)
        {
            input = input.TrimStart();
            int i;
            for (i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    break;
                }
            }
            Identifier = input.Substring(0, i);
            ArgString = input.Substring(Math.Min(input.Length, i + 1));
        }

        protected Token(string id, string args)
        {
            Identifier = id;
            ArgString = args;
        }

        public override bool Equals(object obj)
        {
            var token = obj as Token;
            return token != null &&
                   Identifier == token.Identifier &&
                   ArgString == token.ArgString;
        }

        public override int GetHashCode()
        {
            var hashCode = -672673088;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Identifier);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ArgString);
            return hashCode;
        }
    }
}