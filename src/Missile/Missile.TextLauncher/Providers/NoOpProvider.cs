using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Providers
{
    [Export(typeof(IProvider))]
    public class NoOpProvider : IProvider<object>
    {
        public string Name { get; set; } = "noop";

        public IObservable<object> Provide(string[] args)
        {
            return new object[0].ToObservable();
        }
    }
}