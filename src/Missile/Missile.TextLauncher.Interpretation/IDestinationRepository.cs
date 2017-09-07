using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation
{
    public interface IDestinationRepository
    {
        IList<RegisteredDestination> RegisteredDestinations { get; set; }
        RegisteredDestination Get(string requestedDestinationName);
    }
}