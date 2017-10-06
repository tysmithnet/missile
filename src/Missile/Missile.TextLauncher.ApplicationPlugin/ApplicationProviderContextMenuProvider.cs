using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Missile.TextLauncher.ListPlugin;

namespace Missile.TextLauncher.ApplicationPlugin
{   
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
            MenuItem menuItem = new MenuItem
            {
                Header = "Add to Applications"
            };
            menuItem.Click += (sender, args) =>
            {
                CommandHub.Broadcast(new AddApplicationCommand(item));  
            };
            return menuItem;
        }

        public bool CanHandle(RegisteredApplication item)
        {
            return item != null;
        }

        public MenuItem GetMenuItem(RegisteredApplication item, IListDestinationItem target)
        {
            MenuItem menuItem = new MenuItem
            {
                Header = "Remove from Applications"
            };
            menuItem.Click += (sender, args) =>
            {
                CommandHub.Broadcast(new RemoveApplicationCommand(item));
                CommandHub.Broadcast(new RemoveListDestinationItemCommand(target));
            };
            return menuItem;
        }
    }
}