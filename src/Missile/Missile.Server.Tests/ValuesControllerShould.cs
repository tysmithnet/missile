using System;
using Missile.Server.Controllers;
using Xunit;

namespace Missile.Server.Tests
{
    public class ValuesControllerShould
    {
        [Fact]
        public void Return_Value_On_Get()
        {
            ValuesController valuesController = new ValuesController();
            object value = valuesController.Get(1);
            Assert.Equal("value", value);
        }
    }
}
