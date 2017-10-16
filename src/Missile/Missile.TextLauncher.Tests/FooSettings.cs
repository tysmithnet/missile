using System;
using System.Diagnostics.CodeAnalysis;

namespace Missile.TextLauncher.Tests
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class FooSettings : ISettings
    {
        [Setting]
        public int X { get; set; }
    }                             
}