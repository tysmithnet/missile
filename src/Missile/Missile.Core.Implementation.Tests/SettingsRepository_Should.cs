using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Missile.Core.Implementation.Tests
{
    [ExcludeFromCodeCoverage]
    public class SettingsRepository_Should
    {
        [Fact]
        public void Load_Saved_Settings_If_Possible()
        {
            var fooXml = new FooSettings
            {
                X = 42
            }.Serialize();

            var barXml = new BarSettings
            {
                Y = 42
            }.Serialize();

            var settingsRepo = new SettingsRepository();
            var fsMock = new Mock<IFileSystem>();

            fsMock
                .Setup(system => system.OpenFile(It.IsRegex("FooSettings"), It.IsAny<FileMode>(),
                    It.IsAny<FileAccess>(),
                    It.IsAny<FileShare>())).Returns(fooXml.ToStream());
            fsMock
                .Setup(system => system.OpenFile(It.IsRegex("BarSettings"), It.IsAny<FileMode>(),
                    It.IsAny<FileAccess>(),
                    It.IsAny<FileShare>())).Returns(barXml.ToStream());
            settingsRepo.FileSystem = fsMock.Object;
            settingsRepo.AllSettings = new ISettings[] {new FooSettings(), new BarSettings(), new FooBarSettings()};
            settingsRepo.Get<FooSettings>().X.Should().Be(42);
            settingsRepo.Get<BarSettings>().Y.Should().Be(42);
            settingsRepo.Get<FooBarSettings>().Z.Should().Be(0);
            settingsRepo.GetAll().Count().Should().Be(3);
        }

        [Fact]
        public void Return_All_Registered_Settings()
        {
            var fooXml = new FooSettings
            {
                X = 42
            }.Serialize();

            var barXml = new BarSettings
            {
                Y = 42
            }.Serialize();

            var settingsRepo = new SettingsRepository();
            var fsMock = new Mock<IFileSystem>();

            fsMock
                .Setup(system => system.OpenFile(It.IsRegex("FooSettings"), It.IsAny<FileMode>(),
                    It.IsAny<FileAccess>(),
                    It.IsAny<FileShare>())).Returns(fooXml.ToStream());
            fsMock
                .Setup(system => system.OpenFile(It.IsRegex("BarSettings"), It.IsAny<FileMode>(),
                    It.IsAny<FileAccess>(),
                    It.IsAny<FileShare>())).Returns(barXml.ToStream());
            settingsRepo.FileSystem = fsMock.Object;
            settingsRepo.AllSettings = new ISettings[] {new FooSettings(), new BarSettings(), new FooBarSettings()};
            settingsRepo.GetAll().Count().Should().Be(3);
        }

        [Fact]
        public void Save_All_Serializable_Settings()
        {
            var settingsRepo =
                new SettingsRepository
                {
                    AllSettings = new ISettings[] {new FooSettings(), new BarSettings(), new FooBarSettings()}
                };
            var fileSystemMock = new Mock<IFileSystem>();
            var fooStream = new NonClosingMemoryStream();
            var barStream = new NonClosingMemoryStream();
            settingsRepo.FileSystem = fileSystemMock.Object;
            fileSystemMock
                .Setup(system => system.OpenFile(It.IsRegex("FooSettings"), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Write),
                    It.IsAny<FileShare>())).Returns(fooStream);

            fileSystemMock
                .Setup(system => system.OpenFile(It.IsRegex("FooSettings"), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Read),
                    It.IsAny<FileShare>())).Throws<FileNotFoundException>();

            fileSystemMock
                .Setup(system => system.OpenFile(It.IsRegex("BarSettings"), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Write),
                    It.IsAny<FileShare>())).Returns(barStream);

            fileSystemMock
                .Setup(system => system.OpenFile(It.IsRegex("BarSettings"), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Read),
                    It.IsAny<FileShare>())).Throws<FileNotFoundException>();

            settingsRepo.SaveAll();
            using (var streamReader = new StreamReader(fooStream))
            {
                streamReader.ReadToEnd().Should().NotBeEmpty();
            }
            using (var streamReader = new StreamReader(barStream))
            {
                streamReader.ReadToEnd().Should().NotBeEmpty();
            }
        }

        [Fact]
        public void Save_Serializable_Settings()
        {
            var settingsRepo =
                new SettingsRepository
                {
                    AllSettings = new ISettings[] {new FooSettings(), new BarSettings(), new FooBarSettings()}
                };
            var fileSystemMock = new Mock<IFileSystem>();
            var fooStream = new NonClosingMemoryStream();
            var barStream = new NonClosingMemoryStream();
            settingsRepo.FileSystem = fileSystemMock.Object;
            fileSystemMock
                .Setup(system => system.OpenFile(It.IsRegex("FooSettings"), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Write),
                    It.IsAny<FileShare>())).Returns(fooStream);

            fileSystemMock
                .Setup(system => system.OpenFile(It.IsRegex("FooSettings"), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Read),
                    It.IsAny<FileShare>())).Throws<FileNotFoundException>();

            fileSystemMock
                .Setup(system => system.OpenFile(It.IsRegex("BarSettings"), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Write),
                    It.IsAny<FileShare>())).Returns(barStream);

            fileSystemMock
                .Setup(system => system.OpenFile(It.IsRegex("BarSettings"), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Read),
                    It.IsAny<FileShare>())).Throws<FileNotFoundException>();

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
        public void Throw_If_Requested_Setting_Is_Not_Registered()
        {
            var settingsRepo =
                new SettingsRepository {AllSettings = new ISettings[] {new FooSettings(), new BarSettings()}};
            var fsMock = new Mock<IFileSystem>();
            fsMock.Setup(system => system.OpenFile(It.IsAny<string>(), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Read), It.IsAny<FileShare>()))
                .Throws<FileNotFoundException>();
            var ms = new NonClosingMemoryStream();
            fsMock.Setup(system => system.OpenFile(It.IsAny<string>(), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Write), It.IsAny<FileShare>()))
                .Returns(ms);
            settingsRepo.FileSystem = fsMock.Object;
            settingsRepo.Invoking(repository => repository.Get<FooBarSettings>())
                .ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Throw_If_Trying_Copy_Settings_Of_Different_Types()
        {
            Action a = () => SettingsRepository.CopySettings(new BarSettings(), new FooSettings());
            a.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Throw_If_Trying_To_Save_NonSerializable_Settings()
        {
            var settingsRepo =
                new SettingsRepository {AllSettings = new ISettings[] {new FooBarSettings()}};
            settingsRepo.Invoking(repository => repository.Save<FooBarSettings>())
                .ShouldThrow<SerializationException>();
        }

        [Fact]
        public void Throw_If_Trying_To_Save_Unregistered_Settings()
        {
            var settingsRepo =
                new SettingsRepository {AllSettings = new ISettings[] {new FooSettings(), new BarSettings()}};
            var fsMock = new Mock<IFileSystem>();
            fsMock.Setup(system => system.OpenFile(It.IsAny<string>(), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Read), It.IsAny<FileShare>()))
                .Throws<FileNotFoundException>();
            var ms = new NonClosingMemoryStream();
            fsMock.Setup(system => system.OpenFile(It.IsAny<string>(), It.IsAny<FileMode>(),
                    It.Is<FileAccess>(access => access == FileAccess.Write), It.IsAny<FileShare>()))
                .Returns(ms);
            settingsRepo.FileSystem = fsMock.Object;
            settingsRepo.Invoking(repository => repository.Save<FooBarSettings>())
                .ShouldThrow<ArgumentOutOfRangeException>();
        }
    }
}