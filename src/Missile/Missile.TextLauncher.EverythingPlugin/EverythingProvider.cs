using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using CommandLine;
using Missile.TextLauncher.ListPlugin;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.EverythingPlugin
{
    [Export(typeof(IProvider))]
    public class EverythingProvider : IProvider<object>
    {
        [Import]
        protected internal ISettingsRepository SettingsRepository { get; set; }

        public string Name { get; set; } = "everything";

        public IObservable<object> Provide(string[] args)
        {   
            if (args == null || args.Length == 0)
                throw new ArgumentException($"{nameof(args)} should have at least 1 element");
           
            return Observable.Create<FileInfo>(async (observer, token) =>
            {
                var settings = SettingsRepository.Get<EverythingProviderSettings>();
                var commandArgs = args.Take(args.Length - 1).ToArray();
                var options = new EverythingProviderOptions();
                Parser.Default.ParseArgumentsStrict(commandArgs, options); // todo: handle bad args
                Process process = new Process
                {
                    StartInfo =
                    {
                        FileName = settings.EverythingCommandLineExePath,
                        Arguments = args.Last(),
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                while(!process.HasExited)
                {
                    string line = await process.StandardOutput.ReadLineAsync();
                    observer.OnNext(new FileInfo(line));                                   
                }
                process.WaitForExit(1000);
                observer.OnCompleted();
            });                                                   
        }


        public IEnumerable<FileListDestinationItem> GetFiles()
        {
            return null;
        }
    }
}