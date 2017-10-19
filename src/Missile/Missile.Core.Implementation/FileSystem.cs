using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Missile.Core.FileSystem;

namespace Missile.Core.Implementation
{
    /// <inheritdoc />
    /// <summary>
    ///     Default implementation of IFileSystem
    /// </summary>
    /// <seealso cref="T:Missile.Core.FileSystem.IFileSystem" />
    [Export(typeof(IFileSystem))]
    [ExcludeFromCodeCoverage]
    public class FileSystem : IFileSystem
    {
        /// <summary>
        ///     Opens the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileMode">The file mode.</param>
        /// <param name="fileAccess">The file access.</param>
        /// <param name="fileShare">The file share.</param>
        /// <returns></returns>
        public Stream OpenFile(string path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
        {
            // todo: buffer size needs to be setting or something
            return new FileStream(path, fileMode, fileAccess, fileShare, 4048, true);
        }

        /// <summary>
        ///     Gets the icon.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public ImageSource GetIcon(string path)
        {
            return Imaging.CreateBitmapSourceFromHIcon(
                Icon.ExtractAssociatedIcon(path).Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        /// <summary>
        ///     Determines whether the specified file information is directory.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <returns>
        ///     <c>true</c> if the specified file information is directory; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDirectory(FileInfo fileInfo)
        {
            return fileInfo.Attributes.HasFlag(FileAttributes.Directory);
        }
    }
}