using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Missile.TextLauncher.ListPlugin;

namespace Missile.TextLauncher.ApplicationPlugin
{
    [Export(typeof(IDestinationContextMenuProvider))]
    public class ApplicationProviderContextMenuProvider : IDestinationContextMenuProvider
    {
        [Import]
        protected internal ICommandHub CommandHub { get; set; }

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
            else
            {
                var menuItem = new MenuItem();
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

        public bool CanHandle(FileInfo item)
        {
            return item != null;
        }

        private class Breakdown
        {
            public Breakdown(IEnumerable<IListDestinationItem> items)
            {
                var list = items.ToList();
                ApplicationListDestinationItems = list.OfType<ApplicationListDestinationItem>().ToList();
                FileListDestinationItems = list.OfType<FileListDestinationItem>().ToList();
                CanHandle = list.All(i => i is ApplicationListDestinationItem) ||
                            list.All(i => i is FileListDestinationItem);
            }

            public List<ApplicationListDestinationItem> ApplicationListDestinationItems { get; }
            public List<FileListDestinationItem> FileListDestinationItems { get; }
            public bool CanHandle { get; }
        }
    }
}