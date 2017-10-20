using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reactive.Linq;
using FluentAssertions;
using Missile.Core.FileSystem;
using Moq;
using Xunit;

namespace Missile.ListPlugin.Tests
{
    [ExcludeFromCodeCoverage]
    public class FileListDestinationItemConverter_Should
    {
        [WpfFact]
        public void Convert_FileInfos_Into_FileListDestinationItem()
        {
            var fsMock = new Mock<IFileSystem>();
            fsMock.Setup(system => system.IsDirectory(It.IsAny<FileInfo>()))
                .Returns(true);
            var converter = new FileListDestinationItemConverter();
            converter.FileSystem = fsMock.Object;
            converter.Convert(new[] {new FileInfo("C:\\fake")}.ToObservable()).ToEnumerable().Should().NotBeEmpty();
        }
    }
}