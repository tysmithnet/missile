using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Missile.TextLauncher.Interpretation;

namespace Missile.TextLauncher
{
    [Export(typeof(IDestinationRepository))]
    public class DestinationRepository : IDestinationRepository
    {
        public IList<RegisteredDestination> RegisteredDestinations { get; set; } = new List<RegisteredDestination>();

        public RegisteredDestination Get(string requestedDestinationName)
        {
            return RegisteredDestinations.Single(x => x.Name == requestedDestinationName);
        }
    }
}