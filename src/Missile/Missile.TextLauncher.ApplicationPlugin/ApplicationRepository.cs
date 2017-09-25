using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Missile.TextLauncher.ApplicationPlugin
{
    public class ApplicationRepository : IApplicationRepository
    {
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