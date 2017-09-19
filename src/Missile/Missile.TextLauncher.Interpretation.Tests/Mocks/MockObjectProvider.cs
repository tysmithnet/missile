using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.Interpretation.Tests.Mocks
{
    [Export(typeof(IProvider))]
    public class MockObjectProvider : IProvider<object>
    {
        public string Name { get; set; } = "mockobject";
        public IObservable<object> Provide(string[] args)
        {
            return Enumerable.Range(1, 5).Cast<object>().ToObservable();
        }
    }
}
