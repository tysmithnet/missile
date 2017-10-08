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
        public FileListDestinationItem(FileInfo fileInfo)
        {
            InitializeComponent();
            IconImage.Source = Icon.ExtractAssociatedIcon(fileInfo.FullName).ToImageSource();
            FileNameTextBlock.Text = fileInfo.Name;
            FilePathTextBlock.Text = fileInfo.DirectoryName;
        }

        public Guid Id { get; }
    }
}