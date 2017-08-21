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
            GoogleProvider googleProvider = new GoogleProvider();
            string s = await googleProvider.SearchAsync("long cat");
            Assert.NotNull(s);
        }
    }
}
