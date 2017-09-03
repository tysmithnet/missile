using System.Collections.Generic;

namespace Missile.Client.TextLauncher.Compilation
{
    public interface ILexer
    {
        IEnumerable<Token> Lex(string input);
    }
}
