using System;
using System.Diagnostics.CodeAnalysis;

namespace Missile.TextLauncher.Tests
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class FooSettings : ISettings
    {
        public int X { get; set; }

        public FooSubSettings FooSubSettings { get; set; }
    }

    [Serializable]
    public class FooSubSettings : ISettings
    {
    
    }
}