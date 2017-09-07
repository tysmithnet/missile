using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation
{
    public interface ILexer
    {
        IEnumerable<Token> Lex(string input);
    }
}