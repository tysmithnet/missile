using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Missile.TextLauncher.Destination;
using System.ComponentModel.Composition;

namespace Missile.TextLauncher.Interpretation.Tests.Mocks
{
    [Export(typeof(IDestination))]    
    public class MockObjectDestination : IDestination<object>
    {
        public string Name { get; set; } = "mockobject";
        public Task ProcessAsync(IObservable<object> source)
        {
            return Task.CompletedTask;
        }
    }
}
