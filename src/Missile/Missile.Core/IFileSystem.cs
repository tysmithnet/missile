using System.IO;
using System.Windows.Media;

namespace Missile.Core
{
    /// <summary>
    ///     Abstraction over the file system, primarily for testing
    /// </summary>
    public interface IFileSystem
    {
        Stream OpenFile(string path, FileMode mode, FileAccess accees, FileShare fileShare);

        ImageSource GetIcon(string path);
        bool IsDirectory(FileInfo fileInfo);
    }
}