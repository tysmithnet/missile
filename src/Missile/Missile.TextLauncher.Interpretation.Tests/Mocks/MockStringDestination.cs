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
    public class MockStringDestination : IDestination<string>
    {
        public string Name { get; set; } = "mockstring";

        public Task ProcessAsync(IObservable<string> source, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}