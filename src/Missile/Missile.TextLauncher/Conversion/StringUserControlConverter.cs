using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher.Conversion
{
    [Export(typeof(IConverter))]
    public class StringUserControlConverter : IConverter<string, UIElement>
    {
        public IObservable<UIElement> Convert(IObservable<string> source)
        {
            return source.Select(x => new TextBlock()
            {
                Text = x
            });
        }
    }
}
