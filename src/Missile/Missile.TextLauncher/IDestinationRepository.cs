using System.Collections.Generic;

namespace Missile.TextLauncher
{
    public interface IDestinationRepository
    {
        IList<RegisteredDestination> RegisteredDestinations { get; set; }
        RegisteredDestination Get(string requestedDestinationName);
    }
}