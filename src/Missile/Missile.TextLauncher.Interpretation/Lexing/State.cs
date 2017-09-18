using System;
using System.Text.RegularExpressions;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal abstract class State
    {
        public static readonly Regex IdentifierRegex = new Regex("[a-zA-Z0-9_]");
        public abstract State Transition(char input);

        public static event EventHandler<TokenEventArgs> RaiseTokenEvent;

        protected virtual void OnRaiseTokenEvent(TokenEventArgs e)
        {
            RaiseTokenEvent?.Invoke(this, e);
        }

        public virtual void Flush()
        {
        }
    }
}