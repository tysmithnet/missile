using System;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    /// State representing the lexer found something it could not handle
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.State" />
    internal class ErrorState : State
    {
        /// <inheritdoc />
        /// <summary>
        /// Throws an exception
        /// </summary>
        /// <param name="input">The input to lex</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>
        /// A task that when complete will have the next state in hand
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">If called</exception>
        public override Task<State> TransitionAsync(char input, CancellationToken cancellationToken)
        {
            throw new InvalidOperationException("ErrorState cannot be transitioned from");
        }
    }
}