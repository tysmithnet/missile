using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Missile.TextLauncher.Interpretation.Lexing;
using Missile.TextLauncher.Interpretation.Parsing;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(IInterpretationFacade))]
    public class InterpretationFacade : IInterpretationFacade
    {
        [Import]
        protected internal ILexer Lexer { get; set; }

        [Import]
        protected internal IParser Parser { get; set; }

        [Import]
        protected internal IInterpreter Interpreter { get; set; }

        public async Task ExecuteAsync(string input, CancellationToken cancellationToken)
        {
            var tokens = await Lexer.LexAsync(input, cancellationToken);
            var rootNode = await Parser.ParseAsync(tokens, cancellationToken);
            await Interpreter.InterpretAsync(rootNode, cancellationToken);
            // todo: error handling
        }
    }
}