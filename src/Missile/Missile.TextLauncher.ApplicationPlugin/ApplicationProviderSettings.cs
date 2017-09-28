using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Missile.TextLauncher.ApplicationPlugin
{
    [Export(typeof(ISettings))]
    [Serializable]
    public class ApplicationProviderSettings : ISettings
    {
        [Setting]
        public List<string> SearchPaths { get; set; } = new List<string>
        {                                   

        };

        [Setting]
        public int SearchDepth { get; set; } = 5;
    }
}