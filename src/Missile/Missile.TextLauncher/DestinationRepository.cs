using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Missile.TextLauncher.Interpretation;

namespace Missile.TextLauncher
{
    [Export(typeof(IDestinationRepository))]
    public class DestinationRepository : IDestinationRepository
    {
        internal List<RegisteredDestination> registeredDestinations;

        [ImportMany(typeof(IDestination))]
        public IEnumerable<IDestination> Destinations { get; set; }

        internal IList<RegisteredDestination> RegisteredDestinations =>
            registeredDestinations ??
            (registeredDestinations = GetRegisteredDestinations(Destinations));

        public RegisteredDestination Get(string requestedDestinationName)
        {
            return RegisteredDestinations.Single(x => x.Name == requestedDestinationName);
        }

        public void Add(RegisteredDestination destination)
        {
            if (registeredDestinations == null)
                registeredDestinations = new List<RegisteredDestination>();
            registeredDestinations.Add(destination);
        }

        internal List<RegisteredDestination> GetRegisteredDestinations(IEnumerable<IDestination> destinations)
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