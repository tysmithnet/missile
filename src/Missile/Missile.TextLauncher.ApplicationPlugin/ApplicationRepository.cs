using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Media.Imaging;

namespace Missile.TextLauncher.ApplicationPlugin
{
    [Export(typeof(IApplicationRepository))]
    public class ApplicationRepository : IApplicationRepository
    {
        [Import]
        public ApplicationProviderSettings Settings { get; set; }

        public List<RegisteredApplication> RegisteredApplications { get; set; }

        public IEnumerable<RegisteredApplication> Search(string searchString)
        {
            var image = new BitmapImage(new Uri(@"C:\Users\master\AppData\Local\atom\app.ico"));
            return new List<RegisteredApplication>
            {
                new RegisteredApplication
                {
                    Icon = image,
                    ApplicationName = "Atom",
                    ApplicationPath = @"C:\Users\master\AppData\Local\atom\atom.exe"
                }
            };
        }
    }
}