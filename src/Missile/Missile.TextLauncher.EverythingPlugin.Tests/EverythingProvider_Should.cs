using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reactive.Linq;
using FluentAssertions;
using Moq;
using Xunit;

namespace Missile.TextLauncher.EverythingPlugin.Tests
{
    [ExcludeFromCodeCoverage]
    public class EverythingProvider_Should
    {
        [Fact]
        public void Throw_If_Bad_Arguments_Passed()
        {
            var provider = new EverythingProvider();
            provider.Invoking(everythingProvider => everythingProvider.Provide(null).ToEnumerable())
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Uses_Settings_And_Options_To_Create_Arguments()
        {
            var proxyMock = new Mock<IEverythingProxy>();
            proxyMock.Setup(proxy => proxy.Get(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new FileInfo[0].ToObservable());
            var settingsRepoMock = new Mock<ISettingsRepository>();
            settingsRepoMock.Setup(repository => repository.Get<EverythingProviderSettings>())
                .Returns(new EverythingProviderSettings
                {
                    EverythingCommandLineExePath = "a",
                    DefaultNumMaxResults = 10
                });
            var everythingProvider = new EverythingProvider();
            everythingProvider.EverythingProxy = proxyMock.Object;
            everythingProvider.SettingsRepository = settingsRepoMock.Object;
            var windbg = everythingProvider.Provide("-n 10 -r windbg".Split()).ToEnumerable();
            windbg.Should().BeEmpty();
        }
    }
}