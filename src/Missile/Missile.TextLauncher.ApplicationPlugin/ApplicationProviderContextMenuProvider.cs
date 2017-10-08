using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Controls;
using Missile.TextLauncher.ListPlugin;

namespace Missile.TextLauncher.ApplicationPlugin
{
    // todo: this should be rewritten to handle multiple targets, like if you highlight multiple files and right click in explorer
    [Export(typeof(IDestinationContextMenuProvider<FileInfo>))]
    [Export(typeof(IDestinationContextMenuProvider<RegisteredApplication>))]
    public class ApplicationProviderContextMenuProvider : IDestinationContextMenuProvider<FileInfo>,
        IDestinationContextMenuProvider<RegisteredApplication>
    {
        [Import]
        protected internal ICommandHub CommandHub { get; set; }

        public bool CanHandle(FileInfo item)
        {
            return item != null;
        }

        public MenuItem GetMenuItem(FileInfo item, IListDestinationItem target)
        {
            var menuItem = new MenuItem
            {
                Header = "Add to Applications"
            };
            menuItem.Click += (sender, args) =>
            {
                CommandHub.Broadcast(new AddApplicationCommand(item));
                CommandHub.Broadcast(new SaveApplicationRepositoryStateCommand());
            };
            return menuItem;
        }

        public bool CanHandle(RegisteredApplication item)
        {
            return item != null;
        }

        public MenuItem GetMenuItem(RegisteredApplication item, IListDestinationItem target)
        {
            var menuItem = new MenuItem
            {
                Header = "Remove from Applications"
            };
            menuItem.Click += (sender, args) =>
            {
                CommandHub.Broadcast(new RemoveApplicationCommand(item));
                CommandHub.Broadcast(new RemoveListDestinationItemCommand(target));
                CommandHub.Broadcast(new SaveApplicationRepositoryStateCommand());
            };
            return menuItem;
        }
    }
}