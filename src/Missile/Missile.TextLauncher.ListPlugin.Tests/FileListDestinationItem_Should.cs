using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using FluentAssertions;
using Missile.Core.FileSystem;
using Moq;
using Xunit;

namespace Missile.TextLauncher.ListPlugin.Tests
{
    [ExcludeFromCodeCoverage]
    public class FileListDestinationItem_Should
    {
        [WpfFact]
        public void Use_Default_Image_When_UnauthorizedAccess_Occurs()
        {
            var fsMock = new Mock<IFileSystem>();
            fsMock.Setup(system => system.IsDirectory(It.IsAny<FileInfo>())).Returns(false);
            fsMock.Setup(system => system.GetIcon(It.IsAny<string>())).Throws<UnauthorizedAccessException>();
            var f = new FileListDestinationItem(new FileInfo("C:\\fake\\file.exe"), fsMock.Object);
            f.IconImage.Source.Should().NotBeNull();
        }
    }
}