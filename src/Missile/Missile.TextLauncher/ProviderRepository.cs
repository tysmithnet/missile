using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Missile.TextLauncher.Interpretation;

namespace Missile.TextLauncher
{
    [Export(typeof(IProviderRepository))]
    public class ProviderRepository : IProviderRepository
    {
        public IList<RegisteredProvider> RegisteredProviders { get; set; } = new List<RegisteredProvider>();

        public RegisteredProvider Get(string providerName)
        {
            return RegisteredProviders.Single(x => x.Name == providerName);
        }
    }
}