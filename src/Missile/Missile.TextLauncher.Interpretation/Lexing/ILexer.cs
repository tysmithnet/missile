using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    public interface ILexer
    {
        IEnumerable<Token> Lex(string input);
    }
}