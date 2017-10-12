using System;

namespace Missile.TextLauncher.ListPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Command that removes a list destination item
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.ICommand" />
    public class RemoveListDestinationItemCommand : ICommand
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RemoveListDestinationItemCommand" /> class
        /// </summary>
        /// <param name="target">The target</param>
        public RemoveListDestinationItemCommand(IListDestinationItem target)
        {
            ListDestinationItem = target;
        }

        /// <summary>
        ///     Gets or sets the list destination item
        /// </summary>
        /// <value>
        ///     The list destination item
        /// </value>
        public IListDestinationItem ListDestinationItem { get; set; }

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