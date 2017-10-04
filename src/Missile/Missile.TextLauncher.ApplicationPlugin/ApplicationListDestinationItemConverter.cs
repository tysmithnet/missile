using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reactive.Linq;
using Missile.TextLauncher.Conversion;

namespace Missile.TextLauncher.ApplicationPlugin
{
    [Export(typeof(IConverter))]
    public class ApplicationListDestinationItemConverter : IConverter<RegisteredApplication, ApplicationListDestinationItem>
    {
        [ImportMany]
        protected internal IDestinationContextMenuProvider<RegisteredApplication>[] DestinationContextMenuProviders; 

        public IObservable<ApplicationListDestinationItem> Convert(IObservable<RegisteredApplication> source)
        {
            return source.Select(x =>
                new ApplicationListDestinationItem(x, DestinationContextMenuProviders));
        }
    }
}