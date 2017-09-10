using System.Collections.Generic;
using Missile.TextLauncher.Interpretation;

namespace Missile.TextLauncher
{
    public interface IDestinationRepository
    {
        RegisteredDestination Get(string requestedDestinationName);
        void Add(RegisteredDestination destination);
    }
}