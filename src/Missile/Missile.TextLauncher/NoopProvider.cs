using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

namespace Missile.TextLauncher
{
    [Export(typeof(Provider<>))]
    public class NoOpProvider : Provider<object>
    {
        public override IObservable<object> Provide()
        {
            return new object[0].ToObservable();
        }
    }
}