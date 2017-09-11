using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class LoremIpsumProvider : IProvider<string>
    {
        public string Name { get; set; }
        public IObservable<string> Provide()
        {
            return "Lorem ipsum dolor sit amet.".Split().ToObservable();
        }
    }
}
