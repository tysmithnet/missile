using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal abstract class State
    {
        public static readonly Regex IdentifierRegex = new Regex("[a-zA-Z0-9_]");
        public abstract Task<State> TransitionAsync(char input, CancellationToken cancellationToken);

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