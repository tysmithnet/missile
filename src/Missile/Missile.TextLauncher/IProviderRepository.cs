using System.Collections.Generic;
using Missile.TextLauncher.Interpretation;

namespace Missile.TextLauncher
{
    public interface IProviderRepository
    {
        IList<RegisteredProvider> RegisteredProviders { get; set; }

        RegisteredProvider Get(string providerName);
    }
}