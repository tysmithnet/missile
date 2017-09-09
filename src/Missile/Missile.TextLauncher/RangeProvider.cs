using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;

namespace Missile.TextLauncher
{
    [Export(typeof(Provider<>))]
    public class RangeProvider : Provider<object>
    {
        public override IObservable<object> Provide()
        {
            return Enumerable.Range(1, 10).Cast<object>().ToObservable();
        }
    }
}