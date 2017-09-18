using System;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class TokenEventArgs : EventArgs
    {
        public TokenEventArgs(Token token)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
        }

        public Token Token { get; set; }
    }
}