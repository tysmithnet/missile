using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    ///     Interaction logic for FileListDestinationItem.xaml
    /// </summary>
    public partial class FileListDestinationItem : UserControl
    {
        public FileListDestinationItem(FileInfo fileInfo,
            IEnumerable<IDestinationContextMenuProvider<FileInfo>> fileInfoContextMenuProviders)
        {
            FileInfo = fileInfo;
            FileInfoContextMenuProviders = fileInfoContextMenuProviders.ToList();
            InitializeComponent();
            IconImage.Source = Icon.ExtractAssociatedIcon(FileInfo.FullName).ToImageSource();
            FileNameTextBlock.Text = FileInfo.Name;
            FilePathTextBlock.Text = FileInfo.DirectoryName;
            ContextMenu = new ContextMenu();
            foreach (var p in FileInfoContextMenuProviders)
                if (p.CanHandle(FileInfo))
                {
                    var menuItem = p.GetMenuItem(FileInfo);
                    ContextMenu.Items.Add(menuItem);
                }
            MouseRightButtonUp += (sender, args) =>
            {
                if (ContextMenu != null)
                {
                    ContextMenu.PlacementTarget = this;
                    ContextMenu.IsOpen = true;
                }
            };
        }

        public FileInfo FileInfo { get; set; }

        public IEnumerable<IDestinationContextMenuProvider<FileInfo>> FileInfoContextMenuProviders { get; set; }
    }
}