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
    public class MockStringDestination : IDestination<string>
    {
        public string Name { get; set; } = "mockstring";
        public Task ProcessAsync(IObservable<string> source)
        {
            return Task.CompletedTask;
        }
    }
}
