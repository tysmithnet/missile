using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Missile.Core.Implementation.Tests
{
    [ExcludeFromCodeCoverage]
    public class NonClosingMemoryStream : MemoryStream
    {
        protected override void Dispose(bool disposing)
        {
            Flush();
            Position = 0;
        }
    }
}