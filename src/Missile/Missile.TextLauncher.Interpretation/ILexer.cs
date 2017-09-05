using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation
{
    public interface ILexer
    {
        IEnumerable<IToken> Lex(string input);
    }
}