using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public interface IFacade
    {
        IInterpreter Interpreter { get; set; }
        ILexer Lexer { get; set; }
        IParser Parser { get; set; }

        Task Execute(string input);
    }
}