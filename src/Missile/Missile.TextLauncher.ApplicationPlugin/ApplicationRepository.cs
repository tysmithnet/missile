using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;

namespace Missile.TextLauncher.ApplicationPlugin
{
    [Export(typeof(IApplicationRepository))]
    public class ApplicationRepository : IApplicationRepository
    {
        public List<RegisteredApplication> RegisteredApplications { get; set; }

        [Import]
        protected internal ISettingsRepository SettingsRepository { get; set; }

        public IEnumerable<RegisteredApplication> Search(string searchString)
        {
            var results = new List<RegisteredApplication>();
            var settings = SettingsRepository.Get<ApplicationProviderSettings>();
            foreach (var path in settings.SearchPaths ?? new List<string>())
            {
                var icon = Icon.ExtractAssociatedIcon(path);
                var name = new FileInfo(path).Name;
                results.Add(new RegisteredApplication
                {
                    ApplicationName = name,
                    ApplicationPath = path,
                    Icon = icon
                });
            }
            return results;
        }

        public void Add(FileInfo fileInfo)
        {
            RegisteredApplications.Add(new RegisteredApplication
            {
                ApplicationName = fileInfo.Name,
                ApplicationPath = fileInfo.FullName,
                Icon = Icon.ExtractAssociatedIcon(fileInfo.FullName)
            });
        }
    }
}