using System;
using System.Threading.Tasks;
using Xunit;

namespace Missile.GooglePlugin.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            GoogleService googleProvider = new GoogleService();
            object s = await googleProvider.GetAsync("long cat");
            Assert.NotNull(s);
        }
    }
}
