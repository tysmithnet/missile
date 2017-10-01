using System;

namespace Missile.TextLauncher.Filtration
{
    /// <summary>
    /// Filter that does no transformation
    /// </summary>
    public class NoOpFilter : IFilter<object, object>
    {
        /// <inheritdoc />
        public string Name { get; set; } = "noop";
        
        /// <inheritdoc />
        public IObservable<object> Process(IObservable<object> source)
        {
            return source;
        }
    }
}