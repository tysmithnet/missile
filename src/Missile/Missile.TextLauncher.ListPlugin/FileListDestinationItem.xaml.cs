using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Controls;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    ///     Interaction logic for FileListDestinationItem.xaml
    /// </summary>
    public partial class FileListDestinationItem : UserControl, IListDestinationItem
    {
        public FileListDestinationItem(FileInfo fileInfo,
            IEnumerable<IDestinationContextMenuProvider<FileInfo>> fileInfoContextMenuProviders)
        {
            InitializeComponent();
            IconImage.Source = Icon.ExtractAssociatedIcon(fileInfo.FullName).ToImageSource();
            FileNameTextBlock.Text = fileInfo.Name;
            FilePathTextBlock.Text = fileInfo.DirectoryName;
            ContextMenu = new ContextMenu();
            foreach (var p in fileInfoContextMenuProviders)
                if (p.CanHandle(fileInfo))
                {
                    var menuItem = p.GetMenuItem(fileInfo, this);
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

        public Guid Id { get; }
    }
}