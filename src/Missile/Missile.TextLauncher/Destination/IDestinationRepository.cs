using System.Collections.Generic;

namespace Missile.TextLauncher.Destination
{
    /// <summary>
    ///     Represents a central location for registered destinations
    /// </summary>
    public interface IDestinationRepository
    {
        /// <summary>
        ///     Gets the destination associated with the requested key
        /// </summary>
        /// <param name="requestedDestinationName">Name of the requested destination</param>
        /// <returns>Requested destination</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        RegisteredDestination Get(string requestedDestinationName);

        /// <summary>
        ///     Registers a destination with the repository
        /// </summary>
        /// <param name="destination"></param>
        void Register(RegisteredDestination destination);
    }
}