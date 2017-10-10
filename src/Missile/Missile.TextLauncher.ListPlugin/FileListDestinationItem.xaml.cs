using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    ///     Interaction logic for FileListDestinationItem.xaml
    /// </summary>
    public partial class FileListDestinationItem : UserControl, IListDestinationItem
    {
        public FileListDestinationItem(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
            InitializeComponent();
            try
            {  
                IconImage.Source = fileInfo.Attributes.HasFlag(FileAttributes.Directory)
                    ? ImageSourceFactory.GetBitmapFromResource(typeof(FileListDestinationItem).Assembly, "assets/folder.ico")
                    : Icon.ExtractAssociatedIcon(fileInfo.FullName).ToImageSource();
            }
            catch (UnauthorizedAccessException)
            {
                IconImage.Source = ImageSourceFactory.GetBitmapFromResource(typeof(FileListDestinationItem).Assembly,
                    "assets/unknown.png");
            }                                                                       
            FileNameTextBlock.Text = fileInfo.Name;
            FilePathTextBlock.Text = fileInfo.DirectoryName;
        }

        public FileInfo FileInfo { get; }

        public Guid Id { get; }
    }
}