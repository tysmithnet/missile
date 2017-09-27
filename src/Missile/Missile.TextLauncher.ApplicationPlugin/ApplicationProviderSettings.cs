using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Missile.TextLauncher.ApplicationPlugin
{
    [Export(typeof(ISettings))]
    [Export(typeof(ApplicationProviderSettings))]
    public class ApplicationProviderSettings : ISettings
    {
        [Setting]
        public List<string> SearchPaths { get; set; } = new List<string>
        {
            "hello",
            "world"
        };

        [Setting]
        public int SearchDepth { get; set; } = 5;
    }
}