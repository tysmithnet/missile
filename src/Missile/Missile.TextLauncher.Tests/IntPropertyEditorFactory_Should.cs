using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class IntPropertyEditorFactory_Should
    {
        [Fact]
        public void Should_Indicate_It_Handles_Ints()
        {
            var x = new IntPropertyEditorFactory();
            x.CanHandle(typeof(int)).Should().BeTrue();
            x.CanHandle(typeof(long)).Should().BeFalse();
        }
    }
}