using System;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    ///     Represents something that can be used by ListDestination to display to the user
    /// </summary>
    public interface IListDestinationItem
    {
        /// <summary>
        ///     Gets the identifier for this instance
        /// </summary>
        /// <value>
        ///     The identifier
        /// </value>
        Guid Id { get; }
    }
}