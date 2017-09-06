using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation.Compilation
{
    public interface IParser
    {
        RootNode Parse(IEnumerable<Token> tokens);
    }
}