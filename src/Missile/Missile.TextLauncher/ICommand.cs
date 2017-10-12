using System;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Represents a command that can be broadcast to interested parties
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        ///     Gets the identifier for this command
        /// </summary>
        /// <value>
        ///     The identifier for this command
        /// </value>
        Guid Id { get; }
    }
}