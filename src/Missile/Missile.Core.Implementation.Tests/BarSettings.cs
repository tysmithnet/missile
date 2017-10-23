using System;
using System.Diagnostics.CodeAnalysis;

namespace Missile.TextLauncher.Tests
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class BarSettings : ISettings
    {
        public int Y { get; set; }
    }
}