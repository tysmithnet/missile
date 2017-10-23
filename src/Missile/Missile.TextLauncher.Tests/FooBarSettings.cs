using System.Diagnostics.CodeAnalysis;
using Missile.Core;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class FooBarSettings : ISettings
    {
        public int Z { get; set; }
    }
}