using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Missile.TextLauncher.Filtration;

namespace Missile.TextLauncher.Interpretation.Tests.Mocks
{
    [Export(typeof(IFilter))]
    public class MockObjectFilter : IFilter<object, object>
    {
        public string Name { get; set; } = "mockobject";
        public IObservable<object> Process(IObservable<object> source)
        {
            return source;
        }
    }
}
