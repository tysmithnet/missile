using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class TakeFilter : Filter<object, object>
    {
        public override IObservable<object> Process(IObservable<object> source)
        {
            return source.Take(5);
        }
    }
}
