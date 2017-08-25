using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Missile.GooglePlugin.Tests
{       
    public class GoogleServiceShould
    {                                                                   
        [Fact]
        public async Task Return_What_The_Adapter_Provides()
        {
            // arrange
            GoogleService googleService = new GoogleService();
            var mockAdapter = GoogleTestsUtility.GetMockGoogleAdapter();
            googleService.GoogleAdapter = mockAdapter.Object;
            
            // act
            object actual = await googleService.GetAsync("long cat");

            // assert
            Assert.Equal(GoogleTestsUtility.GoogleResponse, actual);
            mockAdapter.Verify(adapter => adapter.SearchAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
