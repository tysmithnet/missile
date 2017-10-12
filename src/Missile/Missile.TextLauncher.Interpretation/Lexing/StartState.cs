using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    /// The starting state of the lexer state machine
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.State" />
    internal class StartState : State
    {
        /// <inheritdoc />
        /// <summary>
        /// Transitions this state to the next asynchronously
        /// </summary>
        /// <param name="input">The input to lex</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>
        /// A task that when complete will have the next state in hand
        /// </returns>
        public override async Task<State> TransitionAsync(char input, CancellationToken cancellationToken)
        {
            if (input == ' ')
                return this;
            if (IdentifierRegex.IsMatch(input.ToString()))
                return new ProviderState(input.ToString());

            return null;
        }
    }
}