using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
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
