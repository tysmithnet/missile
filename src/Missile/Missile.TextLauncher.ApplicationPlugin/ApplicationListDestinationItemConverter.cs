using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using Missile.TextLauncher.Conversion;

namespace Missile.TextLauncher.ApplicationPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Converter that can turn registered applications into application list destination items
    /// </summary>
    /// <seealso
    ///     cref="!:Missile.TextLauncher.Conversion.IConverter{Missile.TextLauncher.ApplicationPlugin.RegisteredApplication, Missile.TextLauncher.ApplicationPlugin.ApplicationListDestinationItem}" />
    [Export(typeof(IConverter))]
    public class
        ApplicationListDestinationItemConverter : IConverter<RegisteredApplication, ApplicationListDestinationItem>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Converts the specified source
        /// </summary>
        /// <param name="source">The source</param>
        /// <returns>Observable of convertered items</returns>
        public IObservable<ApplicationListDestinationItem> Convert(IObservable<RegisteredApplication> source)
        {
            return source.Select(x =>
                new ApplicationListDestinationItem(x));
        }
    }
}