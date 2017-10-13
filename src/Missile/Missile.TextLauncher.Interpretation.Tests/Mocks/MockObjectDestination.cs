using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Missile.TextLauncher.Destination;

namespace Missile.TextLauncher.Interpretation.Tests.Mocks
{
    [Export(typeof(IDestination))]
    [ExcludeFromCodeCoverage]
    public class MockObjectDestination : IDestination<object>
    {
        public string Name { get; set; } = "mockobject";

        public Task ProcessAsync(IObservable<object> source, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}