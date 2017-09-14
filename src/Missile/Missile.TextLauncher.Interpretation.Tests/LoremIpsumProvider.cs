using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;

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