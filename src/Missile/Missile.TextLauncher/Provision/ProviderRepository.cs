using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Provision
{
    /// <inheritdoc />
    /// <summary>
    ///     Default implementation of IProviderRepository
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Provision.IProviderRepository" />
    [Export(typeof(IProviderRepository))]
    public class ProviderRepository : IProviderRepository
    {
        /// <summary>
        ///     The registered providers
        /// </summary>
        private IList<RegisteredProvider> _registeredProviders;


        /// <summary>
        ///     Gets or sets the provider instances
        /// </summary>
        /// <value>
        ///     The providers instances
        /// </value>
        [ImportMany(typeof(IProvider))]
        protected internal IEnumerable<IProvider> Providers { get; set; }

        /// <summary>
        ///     Gets or sets the registered providers
        /// </summary>
        /// <value>
        ///     The registered providers
        /// </value>
        protected internal IList<RegisteredProvider> RegisteredProviders
        {
            get => _registeredProviders ?? (_registeredProviders = ExtractRegisteredProviders(Providers));
            set => _registeredProviders = value;
        }


        /// <inheritdoc />
        /// <summary>
        ///     Gets a registered provider by name
        /// </summary>
        /// <param name="providerName">Name of the registered provider</param>
        /// <returns></returns>
        public RegisteredProvider Get(string providerName)
        {
            return RegisteredProviders.Single(x => x.Name == providerName);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Adds a RegisteredProvider to this repository
        /// </summary>
        /// <param name="provider">The RegisteredProvider to add</param>
        public void Add(RegisteredProvider provider)
        {
            if (_registeredProviders == null)
                _registeredProviders = new List<RegisteredProvider>();
            _registeredProviders.Add(provider);
        }

        /// <summary>
        ///     Extracts registered providers from a set of provider instances
        /// </summary>
        /// <param name="providers">The providers from which to extract</param>
        /// <returns>Extracted registered providers</returns>
        protected internal List<RegisteredProvider> ExtractRegisteredProviders(IEnumerable<IProvider> providers)
        {
            var mapping = providers.Select(d => new
            {
                Instance = d,
                Interfaces = d.GetType().GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IProvider<>)).ToList()
            }).Where(x => x.Interfaces.Any());

            return (from item in mapping
                from iface in item.Interfaces
                select new RegisteredProvider(item.Instance, iface)).ToList();
        }
    }
}