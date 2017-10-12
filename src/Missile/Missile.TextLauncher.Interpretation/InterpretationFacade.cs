using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Missile.TextLauncher.Interpretation.Lexing;
using Missile.TextLauncher.Interpretation.Parsing;

namespace Missile.TextLauncher.Interpretation
{
    /// <inheritdoc />
    /// <summary>
    /// Default implementation of IInterpretationFacade
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.IInterpretationFacade" />
    [Export(typeof(IInterpretationFacade))]
    public class InterpretationFacade : IInterpretationFacade
    {
        /// <summary>
        /// Gets or sets the lexer.
        /// </summary>
        /// <value>
        /// The lexer.
        /// </value>
        [Import]
        protected internal ILexer Lexer { get; set; }

        /// <summary>
        /// Gets or sets the parser.
        /// </summary>
        /// <value>
        /// The parser.
        /// </value>
        [Import]
        protected internal IParser Parser { get; set; }

        /// <summary>
        /// Gets or sets the interpreter.
        /// </summary>
        /// <value>
        /// The interpreter.
        /// </value>
        [Import]
        protected internal IInterpreter Interpreter { get; set; }

        /// <summary>
        /// Executes the provided textual command
        /// </summary>
        /// <param name="input">The input command to be executed</param>
        /// <param name="cancellationToken">Token to interrogate for cancellation requests</param>
        /// <returns>
        /// A task representing when the execution is complete
        /// </returns>
        public async Task ExecuteAsync(string input, CancellationToken cancellationToken)
        {
            var tokens = await Lexer.LexAsync(input, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            var rootNode = await Parser.ParseAsync(tokens, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            await Interpreter.InterpretAsync(rootNode, cancellationToken);
        }
    }
}