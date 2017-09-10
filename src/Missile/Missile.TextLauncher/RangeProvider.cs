using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;

namespace Missile.TextLauncher
{
    [Export(typeof(IProvider))]
    public class RangeProvider : IProvider<object>
    {
        public string Name { get; set; } = "range";

        public IObservable<object> Provide()
        {
            return Enumerable.Range(1, 10).Cast<object>().ToObservable();
        }
    }
}