using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Provision
{
    [Export(typeof(IProviderRepository))]
    public class ProviderRepository : IProviderRepository
    {
        protected internal List<RegisteredProvider> registeredProviders;

        [ImportMany(typeof(IProvider))]
        protected internal IEnumerable<IProvider> Providers { get; set; }

        protected internal IList<RegisteredProvider> RegisteredProviders =>
            registeredProviders ?? (registeredProviders = GetRegisteredProviders(Providers));

        public RegisteredProvider Get(string providerName)
        {
            return RegisteredProviders.Single(x => x.Name == providerName);
        }

        public void Add(RegisteredProvider provider)
        {
            if (registeredProviders == null)
                registeredProviders = new List<RegisteredProvider>();
            registeredProviders.Add(provider);
        }

        internal List<RegisteredProvider> GetRegisteredProviders(IEnumerable<IProvider> providers)
        {
            var registeredProviders = new List<RegisteredProvider>();

            var mapping = providers.Select(d => new
            {
                Instance = d,
                Interfaces = d.GetType().GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IProvider<>)).ToList()
            }).Where(x => x.Interfaces.Any());

            foreach (var item in mapping)
            foreach (var iface in item.Interfaces)
                registeredProviders.Add(new RegisteredProvider(item.Instance, iface));

            return registeredProviders;
        }
    }
}