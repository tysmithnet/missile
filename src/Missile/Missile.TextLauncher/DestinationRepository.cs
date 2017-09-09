using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Missile.TextLauncher.Interpretation;

namespace Missile.TextLauncher
{
    [Export(typeof(IDestinationRepository))]
    public class DestinationRepository : IDestinationRepository
    {
        [ImportMany(typeof(Destination<object>))]
        public IEnumerable<Destination<object>> Destinations { get; set; }

        internal List<RegisteredDestination> registeredDestinations;

        public IList<RegisteredDestination> RegisteredDestinations =>
            registeredDestinations ??
            (registeredDestinations = Destinations.Select(x => new RegisteredDestination(x)).ToList());

        public RegisteredDestination Get(string requestedDestinationName)
        {
            return RegisteredDestinations.Single(x => x.Name == requestedDestinationName);
        }

        public void Add(RegisteredDestination destination)
        {
            if(registeredDestinations == null)
                registeredDestinations = new List<RegisteredDestination>();
            registeredDestinations.Add(destination);
        }
    }
}