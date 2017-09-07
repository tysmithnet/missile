using System;
using System.Reactive.Linq;

namespace Missile.TextLauncher
{
    public class NoopProvider : Provider<object>
    {
        public override IObservable<object> Provide()
        {
            return new object[0].ToObservable();
        }
    }
}