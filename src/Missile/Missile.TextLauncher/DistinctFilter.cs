using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace Missile.TextLauncher
{
    [Export(typeof(IFilter<object, object>))]
    public class DistinctFilter : IFilter<object, object>
    {
        public string Name { get; set; } = "distinct";

        public IObservable<object> Process(IObservable<object> source)
        {
            return source.Distinct();
        }
    }
}