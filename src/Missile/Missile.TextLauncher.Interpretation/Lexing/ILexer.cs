using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    public interface ILexer
    {
        Task<IEnumerable<Token>> LexAsync(string input, CancellationToken cancellationToken);
    }
}