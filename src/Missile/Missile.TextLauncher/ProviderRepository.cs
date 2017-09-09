using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Missile.TextLauncher.Interpretation;

namespace Missile.TextLauncher
{
    [Export(typeof(IProviderRepository))]
    public class ProviderRepository : IProviderRepository
    {
        [ImportMany(typeof(Provider<object>))]
        public IEnumerable<Provider<object>> Providers { get; set; }

        internal List<RegisteredProvider> registeredProviders;

        public IList<RegisteredProvider> RegisteredProviders =>
            registeredProviders ?? (registeredProviders = Providers.Select(x => new RegisteredProvider(x)).ToList());

        public RegisteredProvider Get(string providerName)
        {
            return RegisteredProviders.Single(x => x.Name == providerName);
        }

        public void Add(RegisteredProvider provider)
        {
            if(registeredProviders == null)
                registeredProviders = new List<RegisteredProvider>();
            registeredProviders.Add(provider);
        }
    }
}