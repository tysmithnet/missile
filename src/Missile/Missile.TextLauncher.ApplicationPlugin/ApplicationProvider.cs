using System;
using System.Collections.Generic;
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
        public IApplicationRepository ApplicationRepository { get; set; } = new ApplicationRepository();
        public string Name { get; set; } = "apps";

        public IObservable<ApplicationListDestinationItem> Provide(string[] args)
        {
            var options = new Options();
            Parser.Default.ParseArgumentsStrict(args, options);
            return args.SelectMany(x => ApplicationRepository.Search(x))
                .Select(x => new ApplicationListDestinationItem(x.Icon, x.ApplicationName, x.ApplicationPath))
                .ToObservable();
        }

        public class Options
        {
            [ValueList(typeof(List<string>))]
            public IList<string> SearchStrings { get; set; }
        }
    }

    [Export(typeof(ISettings))]
    public class ApplicationProviderSettings : ISettings
    {
        [Setting]
        public string[] SearchPaths { get; set; }

        [Setting]
        public int SearchDepth { get; set; } = 5;

        [SubSettings]
        public ApplicationProviderColorSettings ApplicationProviderColorSettings { get; set; } = new ApplicationProviderColorSettings();
    }

    public class ApplicationProviderColorSettings
    {
        [Setting]
        public string Color { get; set; }
    }
    
}