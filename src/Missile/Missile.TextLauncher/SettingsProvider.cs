using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
    [Export(typeof(IProvider))]
    public class SettingsProvider : IProvider<object>
    {
        private List<SettingsViewModel> _settings;

        [Import]
        protected internal IUiFacade UiFacade { get; set; }

        [Import]
        protected internal ISettingsRepository SettingsRepository { get; set; }

        [Import]
        protected internal IPropertyEditorFactoryRepository PropertyEditorFactoryRepository { get; set; }

        protected internal IList<SettingsViewModel> Settings =>
            _settings ?? (_settings = SettingsRepository.GetAll().Select(ExtractSettingsViewModel).ToList());

        public string Name { get; set; } = "settings";

        public IObservable<object> Provide(string[] args)
        {
            var options = new SettingsProviderOptions();
            Parser.Default.ParseArgumentsStrict(args, options);
            if (options.Save)
            {
                SaveSettings();
            }
            else
            {
                var settings = new Settings(Settings);
                UiFacade.SetOutputControl(settings);
            }
            // todo: this is really hacky and ugly, need a way to handle stand alone providers without piping to null destination
            return new object[0].ToObservable();
        }

        private void SaveSettings()
        {
            var settingsToSave = SettingsRepository.GetAll().Where(x => x.GetType().IsSerializable);
            foreach (var settingToSave in settingsToSave)
            {
                var serializer = new XmlSerializer(settingToSave.GetType());
                var fileName = settingToSave.GetType().FullName + ".config";
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    serializer.Serialize(fileStream, settingToSave);
                }
            }
        }

        private SettingsViewModel ExtractSettingsViewModel(object settings)
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

                if (!adapter.IsSetting && !adapter.IsSubSection)
                    continue;

                if (adapter.IsSetting)
                {
                    var setting = new SettingViewModel();
                    setting.Name = member.Name;
                    setting.PropertyEditor = GetPropertyEditor(adapter);
                    settingsViewModel.Settings.Add(setting);
                }
                else
                {
                    var subSettingsViewModel = ExtractSettingsViewModel(adapter.GetValue());
                    settingsViewModel.SubSettings.Add(subSettingsViewModel);
                }
            }
            return settingsViewModel;
        }

        private FrameworkElement GetPropertyEditor(PropertyFieldAdapter adapter)
        {
            return PropertyEditorFactoryRepository.Get(adapter.GetMemberType()).GetControl(adapter);
        }

        private class SettingsProviderOptions
        {
            [Option('s', "save", HelpText = "Save the current settings")]
            public bool Save { get; set; }
        }
    }
}