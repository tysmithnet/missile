using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using CommandLine;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.ApplicationPlugin
{
    // todo: make it so you can right click certain types and get a context menu
    [Export(typeof(IProvider))]
    [Export(typeof(ApplicationProvider))]
    public class ApplicationProvider : IProvider<RegisteredApplication>
    {
        [Import]
        protected internal IApplicationRepository ApplicationRepository { get; set; }

        public string Name { get; set; } = "apps";

        public IObservable<RegisteredApplication> Provide(string[] args)
        {
            var options = new ApplicationProviderOptions();
            Parser.Default.ParseArgumentsStrict(args, options);
            return args.SelectMany(x => ApplicationRepository.Search(x))
                .ToObservable();
        }
    }
}