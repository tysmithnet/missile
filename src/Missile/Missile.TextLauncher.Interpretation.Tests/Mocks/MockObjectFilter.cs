using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using Missile.TextLauncher.Filtration;

namespace Missile.TextLauncher.Interpretation.Tests.Mocks
{
    [Export(typeof(IFilter))]
    [ExcludeFromCodeCoverage]
    public class MockObjectFilter : IFilter<object, object>
    {
        public string Name { get; set; } = "mockobject";

        public IObservable<object> Filter(string[] args, IObservable<object> source)
        {
            return source;
        }
    }
}