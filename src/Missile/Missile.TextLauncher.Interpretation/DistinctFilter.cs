using System;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Interpretation
{
    public class DistinctFilter : Filter<object, object>
    {
        public override IObservable<object> Process(IObservable<object> source)
        {
            return source.Distinct();
        }
    }
}