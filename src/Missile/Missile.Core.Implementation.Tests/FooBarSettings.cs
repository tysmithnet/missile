using System.Diagnostics.CodeAnalysis;

namespace Missile.Core.Implementation.Tests
{
    [ExcludeFromCodeCoverage]
    public class FooBarSettings : ISettings
    {
        public int Z { get; set; }
    }
}