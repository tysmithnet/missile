using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher
{
    [Export(typeof(IProvider))]
    public class SettingsProvider : IProvider<object>
    {
        private List<SettingsViewModel> _settings;

        [Import]
        public IUiFacade UiFacade { get; set; }

        [ImportMany]
        public ISettings[] AllSettings { get; set; }

        [Import]
        public IPropertyEditorFactoryRepository PropertyEditorFactoryRepository { get; set; }

        protected internal IList<SettingsViewModel> Settings => _settings ?? (_settings = GetSettings());

        public string Name { get; set; } = "settings";

        public IObservable<object> Provide(string[] args)
        {
            var settings = new Settings(Settings);
            UiFacade.SetOutputControl(settings);
            return new object[0].ToObservable();
        }

        // todo: don't throw if bad property, gracefully degrade
        // todo: need custom property editors
        // todo: composite pattern groups
        // todo: allow setting to choose its property editor
        private List<SettingsViewModel> GetSettings()
        {
            var results = new List<SettingsViewModel>();
            foreach (var settings in AllSettings)
            {
                var settingsViewModel = ExtractSettingsViewModel(settings);

                results.Add(settingsViewModel);
            }
            return results;
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

        private UIElement GetPropertyEditor(PropertyFieldAdapter adapter)
        {
            return PropertyEditorFactoryRepository.Get(adapter.GetMemberType()).GetControl(adapter);
        }
    }

    public class SettingsViewModel
    {
        public string Name { get; set; }
        public object Instance { get; set; }
        public IList<SettingsViewModel> SubSettings { get; set; } = new List<SettingsViewModel>();
        public IList<SettingViewModel> Settings { get; set; } = new List<SettingViewModel>();
    }

    public class SettingViewModel
    {
        public string Name { get; set; }
        public UIElement PropertyEditor { get; set; }
    }

    public class PropertyFieldAdapter
    {
        private readonly FieldInfo _fieldInfo;
        private readonly object _instance;
        private readonly MemberInfo _memberInfo;
        private readonly PropertyInfo _propertyInfo;

        public PropertyFieldAdapter(MemberInfo memberInfo, object instance)
        {
            _memberInfo = memberInfo;
            _propertyInfo = memberInfo as PropertyInfo;
            _fieldInfo = memberInfo as FieldInfo;
            _instance = instance;
            if (_instance == null)
                throw new ArgumentNullException(
                    $"{nameof(_instance)} cannot be null because it is required for getting/setting values");
            if (_propertyInfo == null && _fieldInfo == null)
                throw new ArgumentException($"{nameof(memberInfo)} must be PropertyInfo or FieldInfo");
        }

        public bool IsSubSection =>
            _memberInfo.CustomAttributes.Any(a => a.AttributeType == typeof(SubSettingsAttribute));

        public bool IsSetting => _memberInfo.CustomAttributes.Any(a => a.AttributeType == typeof(SettingAttribute));

        // todo: convert to properties
        public void SetValue(object value)
        {
            if (_propertyInfo != null)
                _propertyInfo.SetValue(_instance, value);
            else
                _fieldInfo.SetValue(_instance, value);
        }

        public object GetValue()
        {
            return _propertyInfo != null
                ? _propertyInfo.GetMethod.Invoke(_instance, new object[0])
                : _fieldInfo.GetValue(_instance);
        }

        public Type GetMemberType()
        {
            return _propertyInfo != null ? _propertyInfo.PropertyType : _fieldInfo.FieldType;
        }
    }
}