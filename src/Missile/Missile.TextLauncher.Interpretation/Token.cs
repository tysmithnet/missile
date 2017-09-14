using System;

namespace Missile.TextLauncher.Interpretation
{
    public abstract class Token
    {
        protected Token(string input)
        {
            input = input.TrimStart();
            int i;
            for (i = 0; i < input.Length; i++)
                if (input[i] == ' ')
                    break;
            Identifier = input.Substring(0, i);
            ArgString = input.Substring(Math.Min(input.Length, i + 1));
        }

        public string Identifier { get; protected internal set; }
        public string ArgString { get; protected internal set; }

        public override bool Equals(object obj)
        {
            var token = obj as Token;
            return token != null &&
                   GetType() == token.GetType() &&
                   Identifier == token.Identifier &&
                   ArgString == token.ArgString;
        }

        public override int GetHashCode()
        {
            var hashCode = 824443;
            if (Identifier != null)
                hashCode ^= Identifier.GetHashCode() % 820921;
            if (ArgString != null)
                hashCode ^= ArgString.GetHashCode() % 824147;
            return hashCode;
        }
    }
}