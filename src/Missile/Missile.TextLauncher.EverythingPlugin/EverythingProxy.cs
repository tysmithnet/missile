using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reactive.Linq;

namespace Missile.TextLauncher.EverythingPlugin
{
    [Export(typeof(IEverythingProxy))]
    [ExcludeFromCodeCoverage]
    public class EverythingProxy : IEverythingProxy
    {
        public IObservable<FileInfo> Get(string executablePath, string arguments)
        {
            var obs = Observable.Create<FileInfo>(async (observer, token) =>
            {
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = executablePath,
                        Arguments = arguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                while (!process.HasExited)
                    observer.OnNext(new FileInfo(await process.StandardOutput.ReadLineAsync()));
                process.WaitForExit(1000);
                observer.OnCompleted();
            });
            return obs.Publish().RefCount();
        }
    }
}