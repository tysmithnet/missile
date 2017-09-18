using System.Collections.Generic;
using Missile.TextLauncher.Interpretation.Lexing;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    public interface IParser
    {
        RootNode Parse(IEnumerable<Token> tokens);
    }
}