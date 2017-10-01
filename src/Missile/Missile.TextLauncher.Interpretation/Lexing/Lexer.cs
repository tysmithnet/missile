using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    [Export(typeof(ILexer))]
    public class Lexer : ILexer
    {

        public async Task<IEnumerable<Token>> LexAsync(string input, CancellationToken cancellationToken)
        {
            var stateMachine = new StateMachine();
            return await stateMachine.RunAsync(input, cancellationToken);
        }
    }
}