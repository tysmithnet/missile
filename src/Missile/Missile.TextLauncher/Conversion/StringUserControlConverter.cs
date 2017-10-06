using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher.Conversion
{
    [Export(typeof(IConverter))]
    public class StringUserControlConverter : IConverter<string, FrameworkElement>
    {
        public IObservable<FrameworkElement> Convert(IObservable<string> source)
        {
            return source.Select(x => new TextBlock
            {
                Text = x
            });
        }
    }
}