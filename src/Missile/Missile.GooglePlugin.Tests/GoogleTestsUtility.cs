using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Missile.Core;
using Moq;

namespace Missile.GooglePlugin.Tests
{
    internal static class GoogleTestsUtility
    {
        public const string FakeApiKey = "fakeapikey";
        public const string FakeCseKey = "fakecsekey";
        public static readonly string GoogleResponse = File.ReadAllText("sample.json");

        public static Mock<IHttpService> GetMockHttpService()
        {
            var httpServiceMock = new Mock<IHttpService>();
            httpServiceMock.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .Returns(() => Task.FromResult(GoogleResponse));
            return httpServiceMock;
        }
        
        public static Mock<IConfigurationService> GetMockConfigurationService()
        {
            var mockConfigurationService = new Mock<IConfigurationService>();
            mockConfigurationService.Setup(x => x.GetConfigAsync<Options>("google")).Returns(() => Task.FromResult(new Options
            {
                ApiKey = FakeApiKey,
                CseKey = FakeCseKey
            }));
            return mockConfigurationService;
        }

        public static Mock<IGoogleAdapter> GetMockGoogleAdapter()
        {
            var mockGoogleAdapter = new Mock<IGoogleAdapter>();
            mockGoogleAdapter.Setup(x => x.SearchAsync(It.IsAny<string>())).Returns(() => Task.FromResult(GoogleResponse));
            return mockGoogleAdapter;
        }
    }
}
