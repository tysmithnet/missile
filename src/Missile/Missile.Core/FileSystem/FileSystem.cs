using System.ComponentModel.Composition;
using System.IO;

namespace Missile.Core.FileSystem
{
    [Export(typeof(IFileSystem))]
    public class FileSystem : IFileSystem
    {
        public Stream OpenFile(string path, FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
        {
            // todo: buffer size needs to be setting or something
            return new FileStream(path, fileMode, fileAccess, fileShare, 4048, true);
        }
    }
}