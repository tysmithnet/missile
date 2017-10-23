using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using FluentAssertions;
using Missile.Core;
using Missile.Core.FileSystem;
using Missile.TextLauncher;
using Moq;
using Xunit;

namespace Missile.ApplicationPlugin.Tests
{
    [ExcludeFromCodeCoverage]
    public class ApplicationRepository_Should
    {
        [Fact]
        public async Task Find_Matching_Applications()
        {
            var fsMock = new Mock<IFileSystem>();
            var settingsMock = new Mock<ISettingsRepository>();


            fsMock.Setup(system => system.GetIcon(It.IsAny<string>()))
                .Returns(new BitmapImage());

            settingsMock.Setup(settings => settings.Get<ApplicationProviderSettings>())
                .Returns(new ApplicationProviderSettings
                {
                    SearchPaths = new List<string>
                    {
                        "c:\\fake\\path\\thisisnotreal.lnk"
                    }
                });

            var repo = new ApplicationRepository
            {
                SettingsRepository = settingsMock.Object,
                FileSystem = fsMock.Object,
                CommandHub = new CommandHub()
            };

            await repo.SetupAsync(CancellationToken.None);

            repo.Add(new FileInfo("c:\\fake\\path\\thisisfake.exe"));
            repo.Search("thisis").Count().Should().Be(2);
        }

        [Fact]
        public async Task Not_Setup_More_Than_Once()
        {
            var fsMock = new Mock<IFileSystem>();
            var settingsMock = new Mock<ISettingsRepository>();

            fsMock.Setup(system => system.GetIcon(It.IsAny<string>()))
                .Returns(new BitmapImage());

            settingsMock.Setup(settings => settings.Get<ApplicationProviderSettings>())
                .Returns(new ApplicationProviderSettings
                {
                    SearchPaths = new List<string>
                    {
                        "c:\\fake\\path\\thisisnotreal.lnk"
                    }
                });

            var repo = new ApplicationRepository
            {
                SettingsRepository = settingsMock.Object,
                FileSystem = fsMock.Object,
                CommandHub = new CommandHub()
            };

            await repo.SetupAsync(CancellationToken.None);
            await repo.SetupAsync(CancellationToken.None);
            settingsMock.Verify(repository => repository.Get<ApplicationProviderSettings>(), Times.Once);
        }

        [Fact]
        public async Task Respond_To_Commands()
        {
            var fsMock = new Mock<IFileSystem>();
            var settingsMock = new Mock<ISettingsRepository>();

            fsMock.Setup(system => system.GetIcon(It.IsAny<string>()))
                .Returns(new BitmapImage());

            settingsMock.Setup(settings => settings.Get<ApplicationProviderSettings>())
                .Returns(new ApplicationProviderSettings
                {
                    SearchPaths = new List<string>
                    {
                        "c:\\fake\\path\\thisisnotreal.lnk"
                    }
                });

            var repo = new ApplicationRepository
            {
                SettingsRepository = settingsMock.Object,
                FileSystem = fsMock.Object,
                CommandHub = new CommandHub()
            };

            await repo.SetupAsync(CancellationToken.None);

            repo.Add(new FileInfo("c:\\fake\\path\\thisisfake.exe"));

            repo.CommandHub.Broadcast(new AddApplicationCommand(new FileInfo("c:\\fake\\path\\thisisalsonotreal.exe")));
            repo.CommandHub.Broadcast(new SaveApplicationRepositoryStateCommand());
            var first = repo.RegisteredApplications.First();
            repo.CommandHub.Broadcast(new RemoveApplicationCommand(first));
            repo.Settings.SearchPaths.Last().Should().Be("c:\\fake\\path\\thisisalsonotreal.exe");
            settingsMock.Verify(repository => repository.Save<ApplicationProviderSettings>(), Times.Once);
            repo.Settings.SearchPaths.Contains(first.ApplicationPath).Should().BeFalse();
        }
    }
}