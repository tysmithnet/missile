using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace Missile.TextLauncher.ApplicationPlugin
{   
    [Export(typeof(IDestinationContextMenuProvider<FileInfo>))]
    public class FileContextMenuProvider : IDestinationContextMenuProvider<FileInfo>
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
            menuItem.Click += (sender, args) => ApplicationRepository.Add(item);
            return menuItem;
        }
    }
}