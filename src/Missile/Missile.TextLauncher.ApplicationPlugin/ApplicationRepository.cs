using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;

namespace Missile.TextLauncher.ApplicationPlugin
{
    [Export(typeof(IApplicationRepository))]
    public class ApplicationRepository : IApplicationRepository
    {
        private bool _isSetup;

        // todo: shouldn't be public
        public List<RegisteredApplication> RegisteredApplications { get; set; } = new List<RegisteredApplication>();

        [Import]
        protected internal ISettingsRepository SettingsRepository { get; set; }

        public IEnumerable<RegisteredApplication> Search(string searchString)
        {
            if (!_isSetup)
                Setup();
            return RegisteredApplications;
        }

        public void Add(FileInfo fileInfo)
        {
            if (!_isSetup)
                Setup();
            RegisteredApplications.Add(new RegisteredApplication
            {
                ApplicationName = fileInfo.Name,
                ApplicationPath = fileInfo.FullName,
                Icon = Icon.ExtractAssociatedIcon(fileInfo.FullName)
            });
        }

        protected internal void Setup()
        {
            var settings = SettingsRepository.Get<ApplicationProviderSettings>();
            foreach (var path in settings.SearchPaths ?? new List<string>())
            {
                var icon = Icon.ExtractAssociatedIcon(path);
                var name = new FileInfo(path).Name;
                RegisteredApplications.Add(new RegisteredApplication
                {
                    ApplicationName = name,
                    ApplicationPath = path,
                    Icon = icon
                });
            }
            _isSetup = true;
        }
    }
}