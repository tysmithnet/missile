using System.Collections.Generic;
using CommandLine;

namespace Missile.TextLauncher.ApplicationPlugin
{
    public class ApplicationProviderOptions
    {
        [ValueList(typeof(List<string>))]
        public IList<string> SearchStrings { get; set; }
    }
}