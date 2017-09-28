using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Missile.TextLauncher.ApplicationPlugin
{
    [Export(typeof(IApplicationRepository))]
    public class ApplicationRepository : IApplicationRepository
    {                                                              
        public List<RegisteredApplication> RegisteredApplications { get; set; }

        [Import]
        public ISettingsRepository SettingsRepository { get; set; }

        public IEnumerable<RegisteredApplication> Search(string searchString)
        {
            List<RegisteredApplication> results = new List<RegisteredApplication>();
            var settings = SettingsRepository.Get<ApplicationProviderSettings>();
            foreach (var path in settings.SearchPaths ?? new List<string>())
            {
                Icon icon = Icon.ExtractAssociatedIcon(path);
                string name = new FileInfo(path).Name;
                results.Add(new RegisteredApplication()
                {
                    ApplicationName = name,
                    ApplicationPath = path,
                    Icon = icon
                });
            }
            return results;
        }
    }
}