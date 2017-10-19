using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.EverythingPlugin.Tests
{
    [ExcludeFromCodeCoverage]
    public class EverythingCommandLineArgsBuilder_Should
    {
        [Fact]
        public void Handle_Basic_Cases()
        {
            var builder = new EverythingCommandLineArgsBuilder("windbg");
            builder.Build().Should().Be("windbg");

            builder.WithMaxNumberResults(10);
            builder.Build().Should().Be("-n 10 windbg");

            builder.WithCaseInsensitiveSearch();
            builder.Build().Should().Be("-n 10 -i windbg");

            builder.WithWholeWordSearch();
            builder.Build().Should().Be("-n 10 -i -w windbg");

            builder.WithSortByFullPath();
            builder.Build().Should().Be("-n 10 -i -w -s windbg");
        }

        [Fact]
        public void Handle_Regex_Case()
        {
            new EverythingCommandLineArgsBuilder()
                .WithPosixRegex()
                .WithSearchString("[abc]")
                .Build().Should().Be("-r [abc]");

            new EverythingCommandLineArgsBuilder()
                .WithPosixRegex()
                .WithFullPathSearch()
                .WithSearchString("[abc]")
                .Build().Should().Be("-r -p [abc]");
        }
    }
}