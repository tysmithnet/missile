using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class Facade
    {
        public static Task Execute(string input)
        {
            ILexer lexer = new Lexer();
            IEnumerable<IToken> tokens = lexer.Lex(input);
            IParser parser = new Parser();
            RootNode rootNode = parser.Parse(tokens);
            IInterpreter interpreter = new Interpreter();
            return interpreter.Interpret(rootNode);
        }
    }
}
