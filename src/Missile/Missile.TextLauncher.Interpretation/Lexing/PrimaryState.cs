using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    /// Represents the start of creating a provider, filter, or destination token
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.State" />
    internal abstract class PrimaryState : State
    {
        /// <summary>
        /// Gets or sets the identifier of the provider, fitler, or destination token
        /// </summary>
        /// <value>
        /// The identifier
        /// </value>
        public string Identifier { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Transitions this state to the next asynchronously
        /// </summary>
        /// <param name="input">The input to lex</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>
        /// A task that when complete will have the next state in hand
        /// </returns>
        public sealed override Task<State> TransitionAsync(char input, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (input == ' ')
            {
                if (Identifier == null)
                    return Task.FromResult<State>(this);
                return Task.FromResult<State>(GetArgState());
            }

            if (IdentifierRegex.IsMatch(input.ToString()))
            {
                Identifier += input;
                return Task.FromResult<State>(this);
            }

            return Task.FromResult<State>(new ErrorState());
        }

        /// <inheritdoc />
        /// <summary>
        /// Emits the current token
        /// </summary>
        public override void Flush()
        {
            if (!string.IsNullOrWhiteSpace(Identifier))
                OnRaiseTokenEvent(new TokenEventArgs(GetToken()));
        }

        /// <summary>
        /// Gets the token
        /// </summary>
        /// <returns>Get the token for this provider, filter, or destination</returns>
        public abstract Token GetToken();

        /// <summary>
        /// Gets the state of argument collection
        /// </summary>
        /// <returns>The state of argument collection</returns>
        public abstract PrimaryArgState GetArgState();
    }
}