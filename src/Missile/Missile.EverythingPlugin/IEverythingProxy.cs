using System;
using System.IO;

namespace Missile.EverythingPlugin
{
    public interface IEverythingProxy
    {
        IObservable<FileInfo> Get(string executablePath, string arguments);
    }
}