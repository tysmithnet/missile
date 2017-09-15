using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace Missile.TextLauncher
{
    [Export(typeof(IProvider))]
    public class NoOpProvider : IProvider<object>
    {
        public string Name { get; set; } = "noop";

        public IObservable<object> Provide(string argString)
        {
            return new object[0].ToObservable();
        }
    }
}