using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Missile.TextLauncher.ListPlugin;

namespace Missile.TextLauncher.ApplicationPlugin
{
    // todo: this should be rewritten to handle multiple targets, like if you highlight multiple files and right click in explorer
    [Export(typeof(IDestinationContextMenuProvider))]             
    public class ApplicationProviderContextMenuProvider : IDestinationContextMenuProvider
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

        public bool CanHandle(IEnumerable<object> items)
        {
            return items.All(i => i is ApplicationListDestinationItem);
        }

        public IEnumerable<MenuItem> GetMenuItems(IEnumerable<object> items)
        {         
            var menuItem = new MenuItem
            {
                Header = "Remove from Applications"
            };
            foreach (var item in items)
            {
                var cast = item as ApplicationListDestinationItem;
                menuItem.Click += (sender, args) =>
                {
                    CommandHub.Broadcast(new RemoveApplicationCommand(cast.RegisteredApplication));
                    CommandHub.Broadcast(new RemoveListDestinationItemCommand(cast));
                    CommandHub.Broadcast(new SaveApplicationRepositoryStateCommand());
                };
            }
            yield return menuItem;
        }
    }
}