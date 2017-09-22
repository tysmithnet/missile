using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
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
            Options options = new Options();
            Parser.Default.ParseArgumentsStrict(args, options);
            return args.SelectMany(x => ApplicationRepository.Search(x))
                .Select(x => new ApplicationListDestinationItem(x.Icon, x.ApplicationName, x.ApplicationPath)).ToObservable();
        }

        public class Options
        {
            [ValueList(typeof(List<string>))]
            public IList<string> SearchStrings { get; set; }
        }
    }
    
    public class RegisteredApplication
    {
        public BitmapImage Icon { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationPath { get; set; }
    }

    public interface IApplicationRepository
    {
        IEnumerable<RegisteredApplication> Search(string searchString);
    }

    public class ApplicationRepository : IApplicationRepository
    {
        public List<RegisteredApplication> RegisteredApplications { get; set; } 

        public IEnumerable<RegisteredApplication> Search(string searchString)
        {
            BitmapImage image = new BitmapImage(new Uri(@"C:\Users\master\AppData\Local\atom\app.ico"));
            return new List<RegisteredApplication>()
            {
                new RegisteredApplication{Icon = image, ApplicationName = "Atom", ApplicationPath = @"C:\Users\master\AppData\Local\atom\atom.exe"}
            };
        }
    }

    
}
