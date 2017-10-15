using System.IO;

namespace Missile.Core.FileSystem
{
    public interface IFileSystem
    {
        Stream OpenFile(string path, FileMode mode, FileAccess accees, FileShare fileShare);
    }
}