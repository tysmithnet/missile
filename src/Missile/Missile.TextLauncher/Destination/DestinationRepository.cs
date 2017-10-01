using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Destination
{
    /// <inheritdoc />
    [Export(typeof(IDestinationRepository))]
    public class DestinationRepository : IDestinationRepository
    {
        /// <summary>
        ///     Single source of truth for RegisteredDestinations backing field
        /// </summary>
        private List<RegisteredDestination> _registeredDestinations;

        /// <summary>
        ///     All destination instances
        /// </summary>
        [ImportMany]
        protected internal IEnumerable<IDestination> Destinations { get; set; }

        /// <summary>
        ///     Single source of truth for RegisteredDestinations
        /// </summary>
        protected internal virtual IList<RegisteredDestination> RegisteredDestinations =>
            _registeredDestinations ??
            (_registeredDestinations = GetRegisteredDestinations(Destinations));

        /// <inheritdoc />
        public RegisteredDestination Get(string requestedDestinationName)
        {
            return RegisteredDestinations.Single(x => x.Name == requestedDestinationName);
        }

        /// <inheritdoc />
        public void Register(RegisteredDestination destination)
        {
            if (_registeredDestinations == null)
                _registeredDestinations = new List<RegisteredDestination>();
            _registeredDestinations.Add(destination);
        }

        /// <summary>
        ///     Transforms an enumeration of destination instances into RegisteredDestinations
        /// </summary>
        /// <param name="destinations">Destination instances to transform</param>
        /// <returns>Transformed RegisteredDestinations</returns>
        protected internal virtual List<RegisteredDestination> GetRegisteredDestinations(
            IEnumerable<IDestination> destinations)
        {
            var mapping = destinations.Select(d => new
            {
                Instance = d,
                Interfaces = d.GetType().GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDestination<>)).ToList()
            }).Where(x => x.Interfaces.Any());

            var registeredDestinations = new List<RegisteredDestination>();
            foreach (var item in mapping)
            foreach (var iface in item.Interfaces)
                registeredDestinations.Add(new RegisteredDestination(item.Instance, iface));

            return registeredDestinations;
        }
    }
}