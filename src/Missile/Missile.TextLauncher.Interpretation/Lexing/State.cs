using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <summary>
    ///     Represents a state in the lexing state machine
    /// </summary>
    internal abstract class State
    {
        /// <summary>
        ///     The identifier regex
        /// </summary>
        public static readonly Regex IdentifierRegex = new Regex("[a-zA-Z0-9_]");

        /// <summary>
        ///     Transitions this state to the next asynchronously
        /// </summary>
        /// <param name="input">The input to lex</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A task that when complete will have the next state in hand</returns>
        public abstract Task<State> TransitionAsync(char input, CancellationToken cancellationToken);

        /// <summary>
        ///     Occurs when a token has been produced
        /// </summary>
        public static event EventHandler<TokenEventArgs> RaiseTokenEvent;

        /// <summary>
        ///     Raises the <see cref="E:RaiseTokenEvent" /> event
        /// </summary>
        /// <param name="e">The <see cref="TokenEventArgs" /> instance containing the event data</param>
        protected virtual void OnRaiseTokenEvent(TokenEventArgs e)
        {
            RaiseTokenEvent?.Invoke(this, e);
        }

        /// <summary>
        ///     Gives the state an opportunity to emit any tokens if it was the last state
        /// </summary>
        public virtual void Flush()
        {
        }
    }
}