using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Controls;
using Missile.Core.FileSystem;

namespace Missile.TextLauncher.ListPlugin
{
    /// <summary>
    ///     Interaction logic for FileListDestinationItem.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    /// <seealso cref="Missile.TextLauncher.ListPlugin.IListDestinationItem" />
    public partial class FileListDestinationItem : UserControl, IListDestinationItem
    {
        public FileListDestinationItem(FileInfo fileInfo, IFileSystem fileSystem)
        {
            FileInfo = fileInfo;
            InitializeComponent();
            try
            {
                IconImage.Source = fileSystem.IsDirectory(fileInfo)
                    ? ImageSourceFactory.GetBitmapFromResource(typeof(FileListDestinationItem).Assembly,
                        "assets/folder.ico")
                    : fileSystem.GetIcon(fileInfo.FullName);
            }
            catch (UnauthorizedAccessException)
            {
                IconImage.Source = ImageSourceFactory.GetBitmapFromResource(typeof(FileListDestinationItem).Assembly,
                    "assets/unknown.png");
            }
            FileNameTextBlock.Text = fileInfo.Name;
            FilePathTextBlock.Text = fileInfo.DirectoryName;
        }

        /// <summary>
        ///     Gets the file information.
        /// </summary>
        /// <value>
        ///     The file information.
        /// </value>
        public FileInfo FileInfo { get; }

        /// <summary>
        ///     Gets the identifier for this instance
        /// </summary>
        /// <value>
        ///     The identifier
        /// </value>
        [ExcludeFromCodeCoverage]
        public Guid Id { get; }
    }
}