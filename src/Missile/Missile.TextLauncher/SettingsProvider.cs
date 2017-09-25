using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher
{
    [Export(typeof(IProvider))]
    public class SettingsProvider : IProvider<object>
    {
        [Import]
        public IUiFacade UiFacade { get; set; }

        [ImportMany]
        public ISettings[] AllSettings { get; set; }

        public string Name { get; set; } = "settings";

        // todo: need better abstraction
        public Type[] ValidTypes { get; set; } = new[]
        {
            typeof(string),
            typeof(bool),
            typeof(int)
        };

        public IObservable<object> Provide(string[] args)
        {
            foreach (var settings in AllSettings)
            {
                var properties = settings.GetType().GetProperties().Where(x =>
                    x.CustomAttributes.Any(a => a.AttributeType == typeof(SettingAttribute))).ToList();
                var fields = settings.GetType().GetFields().Where(x =>
                    x.CustomAttributes.Any(a => a.AttributeType == typeof(SettingAttribute))).ToList();

                var badPropertyUsage = properties.FirstOrDefault(p => ValidTypes.Contains(p.PropertyType));
                if(badPropertyUsage != null)
                    throw new ConfigurationErrorsException($"{settings.GetType()} has SettingsAttribute on an invalid property: {badPropertyUsage.Name}");

                var badFieldUsage = fields.FirstOrDefault(f => ValidTypes.Contains(f.FieldType));
                if(badFieldUsage != null)
                    throw new ConfigurationErrorsException($"{settings.GetType()} has SettingsAttribute on an invalid field: {badFieldUsage.Name}");

                var viewModel = new SettingsViewModel();
                Settings view = new Settings();
                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Children.Add(new TextBlock() {Text = "test"});
                stackPanel.Children.Add(new TextBox() {Text = "value"});
                view.ItemsListBox.ItemsSource = new[] {stackPanel};
                UiFacade.SetOutputControl(view);
            }
            return new object[0].ToObservable();
        }


    }

    public class SettingsViewModel
    {
        public string Name { get; set; }
        public List<SettingViewModel> SettingViewModels { get; set; }
        
    }

    public class SettingViewModel
    {
        public string Name { get; set; }
        public Control Control { get; set; }
    }
}
