using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher.Conversion
{
    /// <inheritdoc />
    /// <summary>
    /// Converts a string into a framework element
    /// </summary>
    /// <seealso cref="!:Missile.TextLauncher.Conversion.IConverter{System.String, System.Windows.FrameworkElement}" />
    [Export(typeof(IConverter))]
    public class StringFrameworkElementConverter : IConverter<string, FrameworkElement>
    {
        /// <inheritdoc />
        /// <summary>
        /// Converts the specified source
        /// </summary>
        /// <param name="source">The source</param>
        /// <returns></returns>
        public IObservable<FrameworkElement> Convert(IObservable<string> source)
        {
            return source.Select(x => new TextBlock
            {
                Text = x
            });
        }
    }
}