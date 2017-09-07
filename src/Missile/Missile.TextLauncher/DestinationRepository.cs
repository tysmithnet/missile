using System.Collections.Generic;
using System.Linq;

namespace Missile.TextLauncher
{
    public class DestinationRepository : IDestinationRepository
    {
        public IList<RegisteredDestination> RegisteredDestinations { get; set; } = new List<RegisteredDestination>();

        public RegisteredDestination Get(string requestedDestinationName)
        {
            return RegisteredDestinations.Single(x => x.Name == requestedDestinationName);
        }
    }
}