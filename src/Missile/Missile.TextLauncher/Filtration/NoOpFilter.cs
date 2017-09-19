using System;

namespace Missile.TextLauncher.Filtration
{
    public class NoOpFilter : IFilter<object, object>
    {
        public string Name { get; set; } = "noop";

        public IObservable<object> Process(IObservable<object> source)
        {
            return source;
        }
    }
}