using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Missile.TextLauncher.Interpretation.Lexing;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    public interface IParser
    {
        Task<RootNode> ParseAsync(IEnumerable<Token> tokens, CancellationToken cancellationToken);
    }
}