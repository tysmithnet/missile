using System;
using System.Reactive.Linq;

namespace Missile.TextLauncher.Interpretation
{
    public class NoopProvider : Provider<object>
    {
        public override IObservable<object> Provide()
        {
            return new object[0].ToObservable();
        }
    }
}