using System;
using System.Linq;

namespace Missile.TextLauncher.Interpretation
{
    public abstract class Token
    {
        protected internal string Input { get; set; }
        protected internal string Name { get; set; }
        protected internal string[] Args { get; set; }

        protected internal Token(string input, string[] args)
        {
            if(string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            Input = input;
            Args = args ?? new string[0];
        }

        public override bool Equals(object obj)
        {
            var token = obj as Token;
            return token != null &&
                   GetType() == token.GetType() &&
                   Input == token.Name &&
                   Args.SequenceEqual(token.Args);
        }

        public override int GetHashCode()
        {
            var hashCode = 824443;
            if (Name != null)
                hashCode ^= Name.GetHashCode() % 820921;
            foreach (string arg in Args)
            {
                hashCode ^= Name.GetHashCode() % 373903;
            }
                
            return hashCode;
        }
    }
}