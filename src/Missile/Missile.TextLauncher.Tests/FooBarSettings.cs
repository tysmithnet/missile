using System.Diagnostics.CodeAnalysis;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class FooBarSettings : ISettings
    {
        public int Z { get; set; }
    }
}