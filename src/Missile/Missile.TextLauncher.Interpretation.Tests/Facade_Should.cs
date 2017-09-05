using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class NoopProvider : IProvider<object>
    {
        public IObservable<object> Provide()
        {
            return new object[0].ToObservable();
        }
    }

    public class Facade_Should
    {
        [Fact]
        public void Handle_Only_Provider_No_Args()
        {
            string input = "noop";
            Task task = new Facade().Execute(input);
            task.Exception.Should().BeNull();
        }
    }
}
