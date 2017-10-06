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
    public class ApplicationRepository : IApplicationRepository
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

        protected internal IObservable<AddApplicationCommand> AddCommands { get; set; }
        protected internal IObservable<RemoveApplicationCommand> RemoveCommands { get; set; }

        public IEnumerable<RegisteredApplication> Search(string searchString)
        {
            if (!_isSetup)
                Setup();
            return RegisteredApplications;
        }

        protected internal void Add(FileInfo fileInfo)
        {
            if (!_isSetup)
                Setup();
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
            if (!_isSetup)
                Setup();
            RegisteredApplications.Remove(item);
        }

        protected internal void Setup()
        {
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
        }

        private void SetupObservables()
        {
            AddCommands = CommandHub.Get<AddApplicationCommand>();
            RemoveCommands = CommandHub.Get<RemoveApplicationCommand>();
            var syncContext = SynchronizationContext.Current;
            // todo: use async/await
            Task.Factory.StartNew(() => { AddCommands.SubscribeOn(syncContext).ForEachAsync(x => Add(x.FileInfo)); });

            Task.Factory.StartNew(() =>
            {
                RemoveCommands.SubscribeOn(syncContext).ForEachAsync(x => Remove(x.RegisteredApplication));
            });
        }
    }
}