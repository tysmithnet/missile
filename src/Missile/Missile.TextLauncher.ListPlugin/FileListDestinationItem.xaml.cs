using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    /// Interaction logic for FileListDestinationItem.xaml
    /// </summary>
    public partial class FileListDestinationItem : UserControl
    {
        public FileInfo FileInfo { get; set; }

        public FileListDestinationItem(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
            InitializeComponent();
            IconImage.Source = Icon.ExtractAssociatedIcon(FileInfo.FullName).ToImageSource();   
            FileNameTextBlock.Text = FileInfo.Name;
            FilePathTextBlock.Text = FileInfo.DirectoryName;
        }
    }
}
