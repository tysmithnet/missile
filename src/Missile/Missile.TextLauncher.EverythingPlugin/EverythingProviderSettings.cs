using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.EverythingPlugin
{
    [Export(typeof(ISettings))]
    [Serializable]
    public class EverythingProviderSettings : ISettings
    {
        [Setting]
        public int DefaultSearchResults { get; set; } = 100;

        /// <summary>
        /// https://www.voidtools.com/support/everything/command_line_interface/
        /// </summary>
        [Setting]
        public string EverythingCommandLineExePath { get; set; }
    }
}
