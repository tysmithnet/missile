using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class RangeProvider : Provider<int>
    {
        public override IObservable<int> Provide()
        {
            return Enumerable.Range(1, 10).ToObservable();
        }
    }
}
