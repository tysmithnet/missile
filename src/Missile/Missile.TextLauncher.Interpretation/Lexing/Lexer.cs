using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    [Export(typeof(ILexer))]
    public class Lexer : ILexer
    {
        public IEnumerable<Token> Lex(string input)
        {
            var stateMachine = new StateMachine();
            return stateMachine.Run(input);
        }
    }
}