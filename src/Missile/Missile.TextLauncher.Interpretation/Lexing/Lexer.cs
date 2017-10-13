using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    ///     Default implementation of ILexer
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.ILexer" />
    [Export(typeof(ILexer))]
    public class Lexer : ILexer
    {
        /// <summary>
        ///     Lexes the input asynchronously
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public async Task<IEnumerable<Token>> LexAsync(string input, CancellationToken cancellationToken)
        {
            var stateMachine = new StateMachine();
            return await stateMachine.RunAsync(input, cancellationToken);
        }
    }
}