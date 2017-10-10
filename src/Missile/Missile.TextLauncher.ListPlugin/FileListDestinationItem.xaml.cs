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
                    ? new BitmapImage(new Uri(@"pack://application:,,,/"
                                              + Assembly.GetExecutingAssembly().GetName().Name
                                              + ";component/"
                                              + "assets/folder.ico", UriKind.Absolute))
                    : Icon.ExtractAssociatedIcon(fileInfo.FullName).ToImageSource();
            }
            catch (UnauthorizedAccessException)
            {
                // todo: probably just log this exception
            }                                                                       
            FileNameTextBlock.Text = fileInfo.Name;
            FilePathTextBlock.Text = fileInfo.DirectoryName;
        }

        public FileInfo FileInfo { get; }

        public Guid Id { get; }
    }
}