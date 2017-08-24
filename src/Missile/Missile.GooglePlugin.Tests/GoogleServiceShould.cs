using System;
using System.IO;
using System.Threading.Tasks;
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
        private GoogleTestFixture Fixture { get; set; }

        public GoogleServiceShould(GoogleTestFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public async Task Return_Json()
        {
            // arrange
            GoogleService googleService = new GoogleService();
            var adapterMock = new Mock<IGoogleAdapter>();
            adapterMock.Setup(x => x.SearchAsync(It.IsAny<string>())).Returns(() => Task.FromResult(Fixture.GoogleJson));
            googleService.GoogleAdapter = adapterMock.Object;

            // act
            object s = await googleService.GetAsync("long cat");

            // assert
            Assert.Equal(Fixture.GoogleJson, s);
        }
    }
}
