using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Missile.TextLauncher.Filtration;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    public class Registered_Filter_Should
    {
        [Fact]
        public void Correctly_Set_Properties()
        {
            var first = new FirstFilter();
            var reg = new RegisteredFilter(first, typeof(IFilter<object, object>));
        }
    }
}
