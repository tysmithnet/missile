using System;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Filters
{
    public class FirstFilter : IFilter<object, object>
    {
        public string Name { get; set; } = "first";

        public IObservable<object> Process(IObservable<object> source)
        {
            return source.FirstAsync();
        }
    }
}