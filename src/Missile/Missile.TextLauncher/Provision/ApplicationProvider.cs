using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using CommandLine;
using Microsoft.Win32;

namespace Missile.TextLauncher.Provision
{
    [Export(typeof(IProvider))]
    public class ApplicationProvider : IProvider<string>
    {
        public string Name { get; set; } = "apps";

        public IObservable<string> Provide(string[] args)
        {
            var options = new Options();
            Parser.Default.ParseArgumentsStrict(args, options);
            var installedApps = GetInstalledApps();
            if (options.SearchStrings != null && options.SearchStrings.Any())
                installedApps = installedApps.Where(x => options.SearchStrings.Any(x.Contains));
            return installedApps.ToObservable();
        }

        protected internal IEnumerable<string> GetInstalledApps()
        {
            var uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

            using (var rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (var skName in rk.GetSubKeyNames())
                    using (var sk = rk.OpenSubKey(skName))
                    {
                        string result = null;
                        try
                        {
                            result = (string) sk.GetValue("DisplayName");
                        }
                        catch (Exception ex)
                        {
                        }
                        if (result != null)
                            yield return result;
                    }
            }
        }

        private class Options
        {
            [ValueList(typeof(List<string>))]
            public IList<string> SearchStrings { get; set; }
        }
    }
}