using System.Collections.Generic;
using Missile.TextLauncher.Interpretation;

namespace Missile.TextLauncher
{
    public interface IProviderRepository
    {
        RegisteredProvider Get(string name);
        void Add(RegisteredProvider provider);
    }
}