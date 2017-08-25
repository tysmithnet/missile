using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Missile.EverythingPlugin.Tests
{
    public class EverythingServiceShould
    {
        [Fact]
        public async Task Return_What_The_Adapter_Provides()
        {
            // arrange
            var everythingService = new EverythingService();
            var adapterMock = new Mock<IEverythingAdapter>();
            adapterMock.Setup(x => x.Search(It.IsAny<string>())).Returns(() => new List<string>() {"a", "b"});
            everythingService.EverythingAdapter = adapterMock.Object;

            // act
            var results = await everythingService.GetAsync("something");

            // assert
            Assert.Equal(new List<string>() {"a", "b"}, results);
            adapterMock.Verify(x => x.Search("something"), Times.Once);
        }
    }
}
