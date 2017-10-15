using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows;
using System.Xml.Serialization;
using CommandLine;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher
{
    /// <inheritdoc />
    /// <summary>
    ///     Provider that provides an observable sequence of settings
    /// </summary>
    /// <seealso cref="T:System.Object" />
    [Export(typeof(IProvider))]
    public class SettingsProvider : IProvider<ISettings>
    {
        /// <summary>
        ///     Gets or sets the UI facade
        /// </summary>
        /// <value>
        ///     The UI facade
        /// </value>
        [Import]
        protected internal IUiFacade UiFacade { get; set; }

        /// <summary>
        ///     Gets or sets the settings repository
        /// </summary>
        /// <value>
        ///     The settings repository
        /// </value>
        [Import]
        protected internal ISettingsRepository SettingsRepository { get; set; }

        /// <summary>
        ///     Gets or sets the property editor factory repository
        /// </summary>
        /// <value>
        ///     The property editor factory repository
        /// </value>
        [Import]
        protected internal IPropertyEditorFactoryRepository PropertyEditorFactoryRepository { get; set; }

        /// <summary>
        ///     Gets all the settings view models
        /// </summary>
        /// <value>
        ///     All settings view models
        /// </value>
        protected internal IList<SettingsViewModel> Settings =>
            SettingsRepository.GetAll().Select(ExtractSettingsViewModel).ToList();

        /// <summary>
        ///     Gets or sets the settings UI
        /// </summary>
        /// <value>
        ///     The settings UI
        /// </value>
        protected internal Settings SettingsUi { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name for this provider
        /// </summary>
        /// <value>
        ///     The name for this provider
        /// </value>
        [ExcludeFromCodeCoverage]
        public string Name { get; set; } = "settings";

        /// <inheritdoc />
        /// <summary>
        ///     Gets an observable sequence of settings predicated on the provided arguments
        /// </summary>
        /// <param name="args">Arguments for this provider</param>
        /// <returns>
        ///     An observable sequence of settings predicated on the provided arguments
        /// </returns>
        public IObservable<ISettings> Provide(string[] args)
        {
            var options = new SettingsProviderOptions();
            Parser.Default.ParseArgumentsStrict(args, options);
            if (options.Save)
            {
                SaveSettings();
            }
            else
            {
                SettingsUi = new Settings(Settings);
                UiFacade.SetOutputControl(SettingsUi);
            }
            // todo: this is really hacky and ugly, need a way to handle stand alone providers without piping to null destination
            return new ISettings[0].ToObservable();
        }

        /// <summary>
        ///     Saves the settings
        /// </summary>
        protected internal void SaveSettings()
        {
            SettingsRepository.SaveAll();
        }

        /// <summary>
        ///     Extracts the settings view model from the provided settings
        /// </summary>
        /// <param name="settings">The settings from which to extract a view model</param>
        /// <returns>An extracted settings view model</returns>
        protected internal SettingsViewModel ExtractSettingsViewModel(ISettings settings)
        {
            var settingsViewModel = new SettingsViewModel();
            settingsViewModel.Instance = settings;
            foreach (var member in settings.GetType().GetMembers())
            {
                var propertyInfo = member as PropertyInfo;
                var fieldInfo = member as FieldInfo;

                if (propertyInfo == null && fieldInfo == null)
                    continue;

                var adapter = new PropertyFieldAdapter(member, settings);

                if (!adapter.IsSetting)
                    continue;

                if (adapter.IsSetting)
                {
                    var setting = new SettingViewModel();
                    setting.Name = member.Name;
                    setting.PropertyEditor = ExtractPropertyEditor(adapter);
                    settingsViewModel.Settings.Add(setting);
                }
                else
                {
                    var subSettings = adapter.GetValue() as ISettings;
                    Debug.Assert(subSettings != null, "Expected to find subsettings of type ISettings, but did not");
                    var subSettingsViewModel = ExtractSettingsViewModel(subSettings);
                    settingsViewModel.SubSettings.Add(subSettingsViewModel);
                }
            }
            return settingsViewModel;
        }

        /// <summary>
        ///     Extracts a property editor for the specified property or field
        /// </summary>
        /// <param name="adapter">The adapter from which to extract</param>
        /// <returns>Property editor for the specified property or field</returns>
        protected internal FrameworkElement ExtractPropertyEditor(PropertyFieldAdapter adapter)
        {
            return PropertyEditorFactoryRepository.Get(adapter.GetMemberType()).GetControl(adapter);
        }
    }
}