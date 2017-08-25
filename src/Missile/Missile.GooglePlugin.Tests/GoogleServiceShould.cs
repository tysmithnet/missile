using System;
using System.IO;
using System.Threading.Tasks;
using Missile.Core;
using Moq;
using Xunit;

namespace Missile.GooglePlugin.Tests
{
    public class GoogleTestFixture
    {
        public readonly string GoogleJson = File.ReadAllText("sample.json");
    }

    public class GoogleServiceShould : IClassFixture<GoogleTestFixture>
    {
        public const string FakeApiKey = "fakeapikey";
        public const string FakeCseKey = "fakecsekey";

        private GoogleTestFixture Fixture { get; set; }

        public GoogleServiceShould(GoogleTestFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public async Task Return_What_The_Adapter_Provides()
        {
            // arrange
            GoogleService googleService = new GoogleService();
            var mockAdapter = GetMockAdapter();
            googleService.GoogleAdapter = mockAdapter.Object;
            
            // act
            object s = await googleService.GetAsync("long cat");

            // assert
            Assert.Equal(Fixture.GoogleJson, s);
            mockAdapter.Verify(adapter => adapter.SearchAsync(It.IsAny<string>()), Times.Once);
        }
        
        [Fact]
        public async Task Use_ApiKey_And_CseKey_When_Making_Http_Requests()
        {
            // arrange
            GoogleService googleService = new GoogleService();
            var configMock = GetMockConfigurationService();
            var httpMock = GetMockHttpService();
            googleService.GoogleAdapter = new GoogleAdapter(configMock.Object, httpMock.Object);

            // act
            object result = await googleService.GetAsync("hello");

            // assert
            httpMock.Verify(service => service.GetStringAsync($"https://www.googleapis.com/customsearch/v1?key={FakeApiKey}&cx={FakeCseKey}&q=hello"), Times.Once);
        }

        private Mock<IHttpService> GetMockHttpService()
        {
            var httpServiceMock = new Mock<IHttpService>();
            httpServiceMock.Setup(x => x.GetStringAsync(It.IsAny<string>()))
                .Returns(() => Task.FromResult(Fixture.GoogleJson));
            return httpServiceMock;
        }

        private GoogleAdapter GetAdapter()
        {
            var googleAdapter = new GoogleAdapter();
            googleAdapter.ConfigurationService = GetMockConfigurationService().Object;
            googleAdapter.HttpService = GetMockHttpService().Object;
            return googleAdapter;
        }

        private Mock<IGoogleAdapter> GetMockAdapter()
        {
            var mockAdapter = new Mock<IGoogleAdapter>();
            mockAdapter.Setup(x => x.SearchAsync(It.IsAny<string>()))
                .Returns(() => Task.FromResult(Fixture.GoogleJson));
            return mockAdapter;
        }

        private Mock<IConfigurationService> GetMockConfigurationService()
        {
            var mockConfigurationService = new Mock<IConfigurationService>();
            mockConfigurationService.Setup(x => x.GetConfigAsync<Options>("google")).Returns(() => Task.FromResult(new Options
            {
                ApiKey = FakeApiKey,
                CseKey = FakeCseKey
            }));
            return mockConfigurationService;
        }
    }
}
