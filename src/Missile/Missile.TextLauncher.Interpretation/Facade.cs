using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class Facade : IFacade
    {
        public ILexer Lexer { get; set; }
        public IParser Parser { get; set; }
        public IInterpreter Interpreter { get; set; }
                                                   
        public Task Execute(string input)
        {
            var tokens = Lexer.Lex(input);
            var rootNode = Parser.Parse(tokens);
            return Interpreter.Interpret(rootNode);
        }
    }
}
