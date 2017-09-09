using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;

namespace Missile.TextLauncher
{
    [Export(typeof(Provider<object>))]
    public class RangeProvider : Provider<object>
    {
        public override string Name { get; set; } = "range";

        public override IObservable<object> Provide()
        {
            return Enumerable.Range(1, 10).Cast<object>().ToObservable();
        }
    }
}