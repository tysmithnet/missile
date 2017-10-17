using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Missile.TextLauncher.ApplicationPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Command for adding applications
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.ICommand" />
    public class AddApplicationCommand : ICommand
    {
        public AddApplicationCommand(FileInfo item)
        {
            FileInfo = item;
        }

        /// <summary>
        ///     Gets or sets the file information for the file to be added to the list of applications
        /// </summary>
        /// <value>
        ///     The file information for the file to be added to the list of applications
        /// </value>
        public FileInfo FileInfo { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the identifier for this command
        /// </summary>
        /// <value>
        ///     The identifier for this command
        /// </value>
        [ExcludeFromCodeCoverage]
        public Guid Id { get; } = Guid.NewGuid();
    }
}