using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Missile.Core.FileSystem;
using Missile.TextLauncher;

namespace Missile.ApplicationPlugin
{
    /// <inheritdoc cref="IApplicationRepository" />
    /// <summary>
    ///     Default implementation of IApplicationRepository
    /// </summary>
    /// <seealso cref="T:Missile.ApplicationPlugin.IApplicationRepository" />
    /// <seealso cref="T:Missile.TextLauncher.IRequiresSetup" />
    [Export(typeof(IApplicationRepository))]
    [Export(typeof(IRequiresSetup))]
    public class ApplicationRepository : IApplicationRepository, IRequiresSetup
    {
        /// <summary>
        ///     The is setup flag
        /// </summary>
        private bool _isSetup;

        /// <summary>
        ///     The settings backing field
        /// </summary>
        protected internal ApplicationProviderSettings SettingsBackingField;

        /// <summary>
        ///     Gets or sets the registered applications
        /// </summary>
        /// <value>
        ///     The registered applications.
        /// </value>
        [ExcludeFromCodeCoverage]
        protected internal List<RegisteredApplication> RegisteredApplications { get; set; } =
            new List<RegisteredApplication>();

        /// <summary>
        ///     Gets or sets the settings repository
        /// </summary>
        /// <value>
        ///     The settings repository
        /// </value>
        [Import]
        protected internal ISettingsRepository SettingsRepository { get; set; }

        /// <summary>
        ///     Gets or sets the command hub
        /// </summary>
        /// <value>
        ///     The command hub
        /// </value>
        [Import]
        protected internal ICommandHub CommandHub { get; set; }

        [Import]
        protected internal IFileSystem FileSystem { get; set; }

        /// <summary>
        ///     Gets the settings
        /// </summary>
        /// <value>
        ///     The settings
        /// </value>
        protected internal ApplicationProviderSettings Settings =>
            SettingsBackingField ?? (SettingsBackingField = SettingsRepository.Get<ApplicationProviderSettings>());

        /// <inheritdoc />
        /// <summary>
        ///     Searches the specified search string
        /// </summary>
        /// <param name="searchString">The search string</param>
        /// <returns></returns>
        public IEnumerable<RegisteredApplication> Search(string searchString)
        {
            return RegisteredApplications.Where(r => r.ApplicationName.Contains(searchString));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Performs setup operations
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to use when requesting that this operation be cancelled</param>
        /// <returns>
        ///     A Task that indicates the completion of this setup
        /// </returns>
        public Task SetupAsync(CancellationToken cancellationToken)
        {
            if (_isSetup)
                return Task.CompletedTask;

            cancellationToken.ThrowIfCancellationRequested();

            SetupCommandObservables();

            var settings = SettingsRepository.Get<ApplicationProviderSettings>();
            foreach (var path in settings.SearchPaths ?? new List<string>())
            {
                var name = new FileInfo(path).Name;
                RegisteredApplications.Add(new RegisteredApplication
                {
                    ApplicationName = name,
                    ApplicationPath = path,
                    Icon = FileSystem.GetIcon(path)
                });
            }
            _isSetup = true;
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Adds the specified file information
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        protected internal void Add(FileInfo fileInfo)
        {
            RegisteredApplications.Add(new RegisteredApplication
            {
                ApplicationName = fileInfo.Name,
                ApplicationPath = fileInfo.FullName,
                Icon = FileSystem.GetIcon(fileInfo.FullName)
            });
            Settings.SearchPaths.Add(fileInfo.FullName);
        }

        /// <summary>
        ///     Saves this instance
        /// </summary>
        protected internal void Save()
        {
            SettingsRepository.Save<ApplicationProviderSettings>();
        }

        /// <summary>
        ///     Removes the specified item
        /// </summary>
        /// <param name="item">The item</param>
        protected internal void Remove(RegisteredApplication item)
        {
            RegisteredApplications.Remove(item);
            // this might not be the best.. not sure
            Settings.SearchPaths.Remove(item.ApplicationPath);
        }

        /// <summary>
        ///     Setups the command observables
        /// </summary>
        private void SetupCommandObservables()
        {
            CommandHub.Get<AddApplicationCommand>().Subscribe(c => { Add(c.FileInfo); });

            CommandHub.Get<RemoveApplicationCommand>().Subscribe(c => { Remove(c.RegisteredApplication); });

            CommandHub.Get<SaveApplicationRepositoryStateCommand>().Subscribe(c => { Save(); });
        }
    }
}