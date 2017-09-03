using System.Collections.Generic;

namespace Missile.Client.TextLauncher.Compilation
{
    public interface IParser
    {
        RootNode Parse(IEnumerable<Token> list);
    }
}
