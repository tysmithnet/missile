using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.ApplicationPlugin
{
    [Export(typeof(IApplicationRepository))]
    [Export(typeof(IRequiresSetup))]
    public class ApplicationRepository : IApplicationRepository, IRequiresSetup
    {
        private bool _isSetup;

        private ApplicationProviderSettings _settings;

        protected internal List<RegisteredApplication> RegisteredApplications { get; set; } =
            new List<RegisteredApplication>();

        [Import]
        protected internal ISettingsRepository SettingsRepository { get; set; }

        [Import]
        protected internal ICommandHub CommandHub { get; set; }

        protected internal ApplicationProviderSettings Settings =>
            _settings ?? (_settings = SettingsRepository.Get<ApplicationProviderSettings>());

        public IEnumerable<RegisteredApplication> Search(string searchString)
        {            
            return RegisteredApplications;
        }

        protected internal void Add(FileInfo fileInfo)
        {             
            RegisteredApplications.Add(new RegisteredApplication
            {
                ApplicationName = fileInfo.Name,
                ApplicationPath = fileInfo.FullName,
                Icon = Icon.ExtractAssociatedIcon(fileInfo.FullName).ToImageSource()
            });
            Settings.SearchPaths.Add(fileInfo.FullName);
        }

        protected internal void Save()
        {
            SettingsRepository.Save<ApplicationProviderSettings>();
        }

        protected internal void Remove(RegisteredApplication item)
        {       
            RegisteredApplications.Remove(item);
            // this might not be the best.. not sure
            Settings.SearchPaths.Remove(item.ApplicationPath);
        }

        private void SetupObservables()
        {
            var syncContext = SynchronizationContext.Current;                         
            
            Task.Run(async () =>
            {
                await CommandHub.Get<AddApplicationCommand>().SubscribeOn(syncContext).ForEachAsync(x => Add(x.FileInfo));
            });

            Task.Run(async () =>
            {
                await CommandHub.Get<RemoveApplicationCommand>().SubscribeOn(syncContext).ForEachAsync(x => Remove(x.RegisteredApplication));
            });

            Task.Run(async () =>
            {
                await CommandHub.Get<SaveApplicationRepositoryStateCommand>().SubscribeOn(syncContext)
                    .ForEachAsync(x => Save());
            });
        }

        public Task SetupAsync(CancellationToken cancellationToken)
        {
            if (_isSetup)
                return Task.CompletedTask;

            SetupObservables();

            var settings = SettingsRepository.Get<ApplicationProviderSettings>();
            foreach (var path in settings.SearchPaths ?? new List<string>())
            {
                var icon = Icon.ExtractAssociatedIcon(path);
                var name = new FileInfo(path).Name;
                RegisteredApplications.Add(new RegisteredApplication
                {
                    ApplicationName = name,
                    ApplicationPath = path,
                    Icon = icon.ToImageSource()
                });
            }
            _isSetup = true;
            return Task.CompletedTask;
        }
    }
}