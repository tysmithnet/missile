using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation
{
    public interface IProviderRepository
    {
        IList<RegisteredProvider> RegisteredProviders { get; set; }

        RegisteredProvider Get(string providerName);
    }
}