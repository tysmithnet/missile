using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Missile.TextLauncher.Provision
{
    [Export(typeof(IProvider))]
    public class ApplicationProvider : IProvider<string>
    {
        public string Name { get; set; } = "apps";

        public IObservable<string> Provide(string[] args)
        {
            return GetInstalledApps().ToObservable();
        }

        public IEnumerable<string> GetInstalledApps()
        {
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        string result = null;
                        try
                        {
                            result = (string)sk.GetValue("DisplayName");
                        }
                        catch (Exception ex)
                        { }
                        if(result != null)
                            yield return result;
                    }
                }
            }
        }
    }
}
