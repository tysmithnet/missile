using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Missile.TextLauncher.Destination;

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