using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using CommandLine;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.ApplicationPlugin
{
    [Export(typeof(IProvider))]
    public class ApplicationProvider : IProvider<ApplicationListDestinationItem>
    {
        [Import]
        public IApplicationRepository ApplicationRepository { get; set; }
        public string Name { get; set; } = "apps";
                           
        public IObservable<ApplicationListDestinationItem> Provide(string[] args)
        {
            var options = new ApplicationProviderOptions();
            Parser.Default.ParseArgumentsStrict(args, options);
            return args.SelectMany(x => ApplicationRepository.Search(x))
                .Select(x => new ApplicationListDestinationItem(x.Icon.ToImageSource(), x.ApplicationName, x.ApplicationPath))
                .ToObservable();
        }
    }
}