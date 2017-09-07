using System.Collections.Generic;
using Missile.TextLauncher.Interpretation;

namespace Missile.TextLauncher
{
    public interface IDestinationRepository
    {
        IList<RegisteredDestination> RegisteredDestinations { get; set; }
        RegisteredDestination Get(string requestedDestinationName);
    }
}