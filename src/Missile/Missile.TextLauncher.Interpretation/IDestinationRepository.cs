using System.Collections.Generic;
using System.Linq;

namespace Missile.TextLauncher.Interpretation
{
    public interface IDestinationRepository
    {
        IList<RegisteredDestination> RegisteredDestinations { get; set; }
        RegisteredDestination Get(string requestedDestinationName);
    }

    public class DestinationRepository : IDestinationRepository
    {
        public IList<RegisteredDestination> RegisteredDestinations { get; set; }

        public RegisteredDestination Get(string requestedDestinationName)
        {
            return RegisteredDestinations.Single(x => x.Name == requestedDestinationName);
        }
    }
}