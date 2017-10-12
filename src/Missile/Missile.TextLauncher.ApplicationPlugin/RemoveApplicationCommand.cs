using System;

namespace Missile.TextLauncher.ApplicationPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Command instructing a particular registered application be removed
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.ICommand" />
    public class RemoveApplicationCommand : ICommand
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RemoveApplicationCommand" /> class
        /// </summary>
        /// <param name="item">The item</param>
        public RemoveApplicationCommand(RegisteredApplication item)
        {
            RegisteredApplication = item;
        }

        /// <summary>
        ///     Gets or sets the registered application
        /// </summary>
        /// <value>
        ///     The registered application
        /// </value>
        public RegisteredApplication RegisteredApplication { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the identifier for this command
        /// </summary>
        /// <value>
        ///     The identifier for this command
        /// </value>
        public Guid Id { get; } = Guid.NewGuid();
    }
}