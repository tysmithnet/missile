using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace Missile.TextLauncher.ApplicationPlugin
{   
    [Export(typeof(IDestinationContextMenuProvider<FileInfo>))]
    [Export(typeof(IDestinationContextMenuProvider<RegisteredApplication>))]
    public class ApplicationProviderContextMenuProvider : IDestinationContextMenuProvider<FileInfo>,
                                                          IDestinationContextMenuProvider<RegisteredApplication>
    {
        [Import]
        protected internal IApplicationRepository ApplicationRepository { get; set; }

        public bool CanHandle(FileInfo item)
        {
            return item != null;
        }               

        public MenuItem GetMenuItem(FileInfo item)
        {
            MenuItem menuItem = new MenuItem
            {
                Header = "Add to Applications"
            };
            menuItem.Click += (sender, args) =>
            {
                ApplicationRepository.Add(item);
                ApplicationRepository.Save();
            };
            return menuItem;
        }

        public bool CanHandle(RegisteredApplication item)
        {
            return item != null;
        }

        public MenuItem GetMenuItem(RegisteredApplication item)
        {
            MenuItem menuItem = new MenuItem
            {
                Header = "Remove from Applications"
            };
            menuItem.Click += (sender, args) =>
            {
                ApplicationRepository.Remove(item);
                ApplicationRepository.Save();
            };
            return menuItem;
        }
    }
}