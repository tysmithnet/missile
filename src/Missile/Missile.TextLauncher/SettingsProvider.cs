using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows.Controls;
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

        protected internal IList<SettingsViewModel> Settings => _settings ?? (_settings = GetSettings());

        // todo: need better abstraction
        public Type[] ValidTypes { get; set; } =
        {
            typeof(string),
            typeof(bool),
            typeof(int)
        };

        public string Name { get; set; } = "settings";

        public IObservable<object> Provide(string[] args)
        {
            Settings settings = new Settings(Settings[0]);
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
                PropertyInfo propertyInfo = member as PropertyInfo;
                FieldInfo fieldInfo = member as FieldInfo;

                if (propertyInfo == null && fieldInfo == null)
                    continue;
                
                var adapter = new PropertyFieldAdapter(member);

                if (!adapter.IsSetting && !adapter.IsSubSection)
                    continue;

                if (adapter.IsSetting)
                {
                    var setting = new SettingViewModel();
                    setting.Name = member.Name;
                    setting.PropertyEditor = GetPropertyEditor(member, settings);
                    settingsViewModel.Settings.Add(setting);
                }
                else
                {
                    var subSettingsViewModel = ExtractSettingsViewModel(adapter.Getvalue(settings));
                    subSettingsViewModel.SubSettings.Add(subSettingsViewModel);
                }   
            }
            return settingsViewModel;
        }

        private class PropertyFieldAdapter
        {
            private MemberInfo _memberInfo;
            private PropertyInfo _propertyInfo;
            private FieldInfo _fieldInfo;

            public bool IsSubSection => _memberInfo.CustomAttributes.Any(a => a.AttributeType == typeof(SubSettingsAttribute));
            public bool IsSetting => _memberInfo.CustomAttributes.Any(a => a.AttributeType == typeof(SettingAttribute));

            public PropertyFieldAdapter(MemberInfo memberInfo)
            {
                _memberInfo = memberInfo;
                _propertyInfo = memberInfo as PropertyInfo;
                _fieldInfo = memberInfo as FieldInfo;
                if(_propertyInfo == null && _fieldInfo == null)
                    throw new ArgumentException($"{nameof(memberInfo)} must be PropertyInfo or FieldInfo");
            }

            public void SetValue(object instance, object value)
            {
                if(_propertyInfo != null)                
                    _propertyInfo.SetValue(instance, value);
                else
                    _fieldInfo.SetValue(instance, value);
            }

            public object Getvalue(object instance)
            {
                if (_propertyInfo != null)
                    return _propertyInfo.GetMethod.Invoke(instance, new object[0]);
                else
                    return _fieldInfo.GetValue(instance);
            }
        }

        private Control GetPropertyEditor(MemberInfo member, object instance)
        {
            var propertyInfo = member as PropertyInfo;
            var fieldInfo = member as FieldInfo;

            if (propertyInfo != null)
            {
                var editor = new TextBox();
                editor.TextChanged += (sender, args) =>
                {
                    propertyInfo.SetMethod.Invoke(instance, new object[] {editor.Text});
                };
                return editor;
            }
            else
            {
                Debug.Assert(fieldInfo != null,
                    $"{nameof(fieldInfo)} cannot be null because member must be property or field");

                var editor = new TextBox();
                editor.TextChanged += (sender, args) => { fieldInfo.SetValue(instance, editor.Text); };
                return editor;
            }            
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
        public Control PropertyEditor { get; set; }
    }
}