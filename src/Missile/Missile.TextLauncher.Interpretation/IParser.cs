using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation
{
    public interface IParser
    {
        RootNode Parse(IEnumerable<IToken> tokens);
    }
}