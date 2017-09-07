using System;
using System.Reactive.Linq;

namespace Missile.TextLauncher
{
    public class TakeFilter : Filter<object, object>
    {
        public override IObservable<object> Process(IObservable<object> source)
        {
            return source.Take(5);
        }
    }
}