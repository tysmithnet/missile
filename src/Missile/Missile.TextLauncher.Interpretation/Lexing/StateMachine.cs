using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <summary>
    /// Represents the state machine that the lexer uses to generate tokens
    /// </summary>
    internal class StateMachine
    {
        /// <summary>
        /// Gets or sets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public State CurrentState { get; set; }

        /// <summary>
        /// Runs the state machine asynchronously
        /// </summary>
        /// <param name="input">The input to the state machine</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<Token>> RunAsync(string input, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            input = input ?? "";
            var tokens = new List<Token>();
            CurrentState = new StartState();
            State.RaiseTokenEvent += (sender, args) => tokens.Add(args.Token);
            foreach (char c in input)
            {
                cancellationToken.ThrowIfCancellationRequested();
                CurrentState = await CurrentState.TransitionAsync(c, cancellationToken);
            }

            CurrentState.Flush();
            return tokens;
        }
    }
}