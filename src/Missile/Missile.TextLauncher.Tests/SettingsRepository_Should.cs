using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using FluentAssertions;
using Missile.Core.FileSystem;
using Moq;
using Xunit;

namespace Missile.TextLauncher.Tests
{            
    [ExcludeFromCodeCoverage]
    public class SettingsRepository_Should
    {
        [Serializable]
        public class FooSettings : ISettings
        {
            public int X { get; set; }
        }

        [Serializable]
        public class BarSettings : ISettings
        {
            public int Y { get; set; }
        }

        public class FooBarSettings : ISettings
        {
            public int Z { get; set; }
        }

        public class NonClosingMemoryStream : MemoryStream
        {
            public NonClosingMemoryStream(string start = null)
            {
                if (start != null)
                {
                    Write(start);    
                }      
            }

            public void Write(string s)
            {
                var bytes = Encoding.UTF8.GetBytes(s);
                Write(bytes, 0, bytes.Length);
            }

            protected override void Dispose(bool disposing)
            {
                Flush();
                Position = 0;
            }
        }

        [Fact]
        public void Save_Serializable_Settings()
        {
            var settingsRepo =
                new SettingsRepository {AllSettings = new ISettings[] {new FooSettings(), new BarSettings(), new FooBarSettings(),}};
            var fileSystemMock = new Mock<IFileSystem>();
            var fooStream = new NonClosingMemoryStream();
            var barStream = new NonClosingMemoryStream();       
            settingsRepo.FileSystem = fileSystemMock.Object;
            fileSystemMock
                .Setup(system => system.OpenFile(It.IsRegex("FooSettings"), It.IsAny<FileMode>(), It.IsAny<FileAccess>(),
                    It.IsAny<FileShare>())).Returns(fooStream);
            fileSystemMock
                .Setup(system => system.OpenFile(It.IsRegex("BarSettings"), It.IsAny<FileMode>(), It.IsAny<FileAccess>(),
                    It.IsAny<FileShare>())).Returns(barStream);
            
            settingsRepo.Save<FooSettings>();
            using (var streamReader = new StreamReader(fooStream))
            {
                streamReader.ReadToEnd().Should().NotBeEmpty();
            }                                
            settingsRepo.Save<BarSettings>();
            using (var streamReader = new StreamReader(barStream))
            {
                streamReader.ReadToEnd().Should().NotBeEmpty();
            }
            settingsRepo.Invoking(repository => repository.Save<FooBarSettings>())
                .ShouldThrow<SerializationException>();
        }

        [Fact]
        public void Throw_If_Trying_To_Save_Unregistered_Settings()
        {
            var settingsRepo =
                new SettingsRepository { AllSettings = new ISettings[] { new FooSettings(), new BarSettings(), } };
            settingsRepo.Invoking(repository => repository.Save<FooBarSettings>())
                .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Throw_If_Trying_To_Save_NonSerializable_Settings()
        {
            var settingsRepo =
                new SettingsRepository { AllSettings = new ISettings[] { new FooBarSettings(), } };
            settingsRepo.Invoking(repository => repository.Save<FooBarSettings>())
                .ShouldThrow<SerializationException>();
        }
    }
}