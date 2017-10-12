using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Missile.TextLauncher.ListPlugin;

namespace Missile.TextLauncher.ApplicationPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Context menu provider for application provider
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.ListPlugin.IDestinationContextMenuProvider" />
    [Export(typeof(IDestinationContextMenuProvider))]
    public class ApplicationProviderContextMenuProvider : IDestinationContextMenuProvider
    {
        /// <summary>
        ///     Gets or sets the command hub
        /// </summary>
        /// <value>
        ///     The command hub
        /// </value>
        [Import]
        protected internal ICommandHub CommandHub { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets remove application menu items and add application menu items
        /// </summary>
        /// <param name="items">The items to produce MenuItems for</param>
        /// <returns>
        ///     An enumeration of 0 or more MenuItems
        /// </returns>
        public IEnumerable<MenuItem> GetMenuItems(IEnumerable<IListDestinationItem> items)
        {
            var breakdown = new Breakdown(items);
            if (!breakdown.CanHandle)
                yield break;

            if (breakdown.ApplicationListDestinationItems.Any())
            {
                var menuItem = new MenuItem();
                menuItem.Header = "Remove Application";
                foreach (var item in breakdown.ApplicationListDestinationItems)
                    menuItem.Click += (sender, args) =>
                    {
                        CommandHub.Broadcast(new RemoveApplicationCommand(item.RegisteredApplication));
                        CommandHub.Broadcast(new RemoveListDestinationItemCommand(item));
                        CommandHub.Broadcast(new SaveApplicationRepositoryStateCommand());
                    };
                yield return menuItem;
            }
            else if (breakdown.FileListDestinationItems.Any())
            {
                var menuItem = new MenuItem {Header = "Add Application"};
                foreach (var item in breakdown.FileListDestinationItems)
                    menuItem.Click += (sender, args) =>
                    {
                        CommandHub.Broadcast(new AddApplicationCommand(item.FileInfo));
                        CommandHub.Broadcast(new SaveApplicationRepositoryStateCommand());
                    };
                yield return menuItem;
            }
        }

        /// <summary>
        ///     Determines whether this instance can handle the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///     <c>true</c> if this instance can handle the specified item; otherwise, <c>false</c>.
        /// </returns>
        public bool CanHandle(FileInfo item)
        {
            return item != null;
        }

        /// <summary>
        ///     Partitions items into ApplicationListDestinationItem and FileListDestinationItem piles
        /// </summary>
        private class Breakdown
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="Breakdown" /> class
            /// </summary>
            /// <param name="items">The items</param>
            public Breakdown(IEnumerable<IListDestinationItem> items)
            {
                var list = items.ToList();
                ApplicationListDestinationItems = list.OfType<ApplicationListDestinationItem>().ToList();
                FileListDestinationItems = list.OfType<FileListDestinationItem>().ToList();
                CanHandle = list.All(i => i is ApplicationListDestinationItem) ||
                            list.All(i => i is FileListDestinationItem);
            }

            /// <summary>
            ///     Gets the application list destination items
            /// </summary>
            /// <value>
            ///     The application list destination items
            /// </value>
            public List<ApplicationListDestinationItem> ApplicationListDestinationItems { get; }

            /// <summary>
            ///     Gets the file list destination items
            /// </summary>
            /// <value>
            ///     The file list destination items
            /// </value>
            public List<FileListDestinationItem> FileListDestinationItems { get; }

            /// <summary>
            ///     Gets a value indicating whether this instance can handle
            /// </summary>
            /// <value>
            ///     <c>true</c> if this instance can handle; otherwise, <c>false</c>
            /// </value>
            public bool CanHandle { get; }
        }
    }
}