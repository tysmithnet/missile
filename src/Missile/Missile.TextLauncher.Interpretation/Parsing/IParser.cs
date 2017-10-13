using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Missile.TextLauncher.Interpretation.Lexing;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    /// <summary>
    ///     Represents an object that is capable of turning an enumeration of tokens into an abstract syntax tree
    /// </summary>
    public interface IParser
    {
        /// <summary>
        ///     Parses the provided tokens asynchronously
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that when complete will hold the root of the AST</returns>
        Task<RootNode> ParseAsync(IEnumerable<Token> tokens, CancellationToken cancellationToken);
    }
}