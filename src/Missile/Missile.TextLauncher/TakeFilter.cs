using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace Missile.TextLauncher
{
    [Export(typeof(IFilter<object, object>))]
    public class TakeFilter : IFilter<object, object>
    {
        public string Name { get; set; } = "take";

        public IObservable<object> Process(IObservable<object> source)
        {
            return source.Take(5);
        }
    }
}