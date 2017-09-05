using System;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Interpretation.Tests.Mocks
{
    public class NoopProvider : IProvider<object>
    {
        public IObservable<object> Provide()
        {
            return new object[0].ToObservable();
        }
    }
}