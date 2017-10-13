using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher.Conversion
{
    [Export(typeof(IConverter))]
    [ExcludeFromCodeCoverage] // this needs to be run on an STA thread which is incompatible with dotCover
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