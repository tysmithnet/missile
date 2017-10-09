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
                                                                            
        public IEnumerable<MenuItem> GetMenuItems(IEnumerable<IListDestinationItem> items)
        {
            Breakdown breakdown = new Breakdown(items);
            if (!breakdown.CanHandle)
                yield break;

            if (breakdown.ApplicationListDestinationItems.Any())
            {
                MenuItem menuItem = new MenuItem();
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
            else
            {
                MenuItem menuItem = new MenuItem();
                menuItem.Header = "Add Application";
                foreach (var item in breakdown.FileListDestinationItems)
                    menuItem.Click += (sender, args) =>
                    {
                        CommandHub.Broadcast(new AddApplicationCommand(item.FileInfo));
                        CommandHub.Broadcast(new SaveApplicationRepositoryStateCommand());
                    };
                yield return menuItem;
            }
        }

        private class Breakdown
        {
            public List<ApplicationListDestinationItem> ApplicationListDestinationItems { get; set; }
            public List<FileListDestinationItem> FileListDestinationItems { get; set; }
            public bool CanHandle { get; set; }

            public Breakdown(IEnumerable<IListDestinationItem> items)
            {
                var list = items.ToList();
                ApplicationListDestinationItems = list.OfType<ApplicationListDestinationItem>().ToList();
                FileListDestinationItems = list.OfType<FileListDestinationItem>().ToList();
                CanHandle = list.All(i => i is ApplicationListDestinationItem) || list.All(i => i is FileListDestinationItem);
            }
        }
    }
}