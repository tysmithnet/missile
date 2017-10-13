using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.Interpretation.Tests.Mocks
{
    [Export(typeof(IProvider))]
    [ExcludeFromCodeCoverage]
    public class MockObjectProvider : IProvider<object>
    {
        public string Name { get; set; } = "mockobject";

        public IObservable<object> Provide(string[] args)
        {
            return Enumerable.Range(1, 5).Cast<object>().ToObservable();
        }
    }
}