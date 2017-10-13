using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <summary>
    ///     Object capable of transforming text input into an enumeration of tokens
    /// </summary>
    public interface ILexer
    {
        /// <summary>
        ///     Lexes the input asynchronously
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<Token>> LexAsync(string input, CancellationToken cancellationToken);
    }
}