using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using FluentAssertions;
using Moq;
using Xunit;

namespace Missile.TextLauncher.ApplicationPlugin.Tests
{
    [ExcludeFromCodeCoverage]
    public class ApplicationProvider_Should
    {
        [Fact]
        public void Find_The_Registered_Application()
        {
            var provider = new ApplicationProvider();
            var mockRepo = new Mock<IApplicationRepository>();
            mockRepo.Setup(repository => repository.Search(It.IsAny<string>())).Returns(new[]
            {
                new RegisteredApplication
                {
                    ApplicationName = "windbg",
                    ApplicationPath = "c:\\windbg.exe"
                }
            });
            provider.ApplicationRepository = mockRepo.Object;
            provider.Provide("windbg".Split()).ToEnumerable().First().Should().BeOfType<RegisteredApplication>();
        }
    }
}