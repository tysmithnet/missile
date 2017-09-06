using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation.Compilation
{
    public interface ILexer
    {
        IEnumerable<Token> Lex(string input);
    }
}