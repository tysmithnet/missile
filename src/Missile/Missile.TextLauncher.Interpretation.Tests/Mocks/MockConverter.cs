using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using Missile.TextLauncher.Conversion;

namespace Missile.TextLauncher.Interpretation.Tests.Mocks
{
    [Export(typeof(IConverter))]
    public class MockConverter : IConverter<object, string>
    {
        public IObservable<string> Convert(IObservable<object> source)
        {
            return source.Select(x => x.ToString());
        }
    }
}