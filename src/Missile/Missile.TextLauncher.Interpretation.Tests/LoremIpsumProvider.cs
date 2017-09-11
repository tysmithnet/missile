using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Tests
{
    [Export(typeof(IProvider))]
    public class LoremIpsumProvider : IProvider<string>
    {
        public string Name { get; set; } = "lorem";
        public IObservable<string> Provide()
        {
            return "Lorem ipsum dolor sit amet.".Split().ToObservable();
        }
    }
}
