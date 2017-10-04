using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace Missile.TextLauncher.ApplicationPlugin
{   
    [Export(typeof(IDestinationContextMenuProvider<FileInfo>))]
    public class ApplicationProviderContextMenuProvider : IDestinationContextMenuProvider<FileInfo>,
                                                          IDestinationContextMenuProvider<ApplicationListDestinationItem>
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

        public bool CanHandle(ApplicationListDestinationItem item)
        {
            return item != null;
        }

        public MenuItem GetMenuItem(ApplicationListDestinationItem item)
        {
            MenuItem menuItem = new MenuItem
            {
                Header = "Remove from Applications"
            };
            menuItem.Click += (sender, args) =>
            {
                
            };
            return menuItem;
        }
    }
}