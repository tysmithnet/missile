using System.Configuration;
using System.Threading.Tasks;
using Missile.Core;
using Moq;
using Xunit;

namespace Missile.GooglePlugin.Tests
{
    public class GoogleAdapterShould
    {
        [Fact]
        public async Task Throw_Configuration_Exception_If_Either_Key_Is_Not_Found()
        {
            GoogleAdapter noApiKey = new GoogleAdapter();
            var noApiConfigService = new Mock<IConfigurationService>();
            noApiConfigService.Setup(x => x.GetConfigAsync<Options>("google"))
                .Returns(() => Task.FromResult(new Options(){CseKey = "here"}));
            noApiKey.ConfigurationService = noApiConfigService.Object;

            GoogleAdapter noCseKey = new GoogleAdapter();
            var noCseConfigService = new Mock<IConfigurationService>();
            noCseConfigService.Setup(x => x.GetConfigAsync<Options>("google"))
                .Returns(() => Task.FromResult(new Options(){ApiKey = "here"}));
            noCseKey.ConfigurationService = noCseConfigService.Object;

            await Assert.ThrowsAsync<ConfigurationErrorsException>(() => noApiKey.SearchAsync("whatever"));
            await Assert.ThrowsAsync<ConfigurationErrorsException>(() => noCseKey.SearchAsync("whatever"));
        }

        [Fact]
        public async Task Use_ApiKey_And_CseKey_When_Making_Http_Requests()
        {
            // arrange
            GoogleService googleService = new GoogleService();
            var configMock = GoogleTestsUtility.GetMockConfigurationService();
            var httpMock = GoogleTestsUtility.GetMockHttpService();
            googleService.GoogleAdapter = new GoogleAdapter(configMock.Object, httpMock.Object);

            // act
            object result = await googleService.GetAsync("hello");

            // assert                                               
            httpMock.Verify(service => service.GetStringAsync($"https://www.googleapis.com/customsearch/v1?key={GoogleTestsUtility.FakeApiKey}&cx={GoogleTestsUtility.FakeCseKey}&q=hello"), Times.Once);
        }
    }
}