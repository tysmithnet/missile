using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings(IEnumerable<SettingsViewModel> settings)
        {
            SettingsViewModels = settings.ToList();
            InitializeComponent();
            foreach (var settingsViewModel in SettingsViewModels)
                AddSettingsEditorPanels(settingsViewModel, SettingsPanel);
        }

        public IList<SettingsViewModel> SettingsViewModels { get; set; }

        private void AddSettingsEditorPanels(SettingsViewModel settingsViewModel, StackPanel container)
        {
            var newStackPanel = new StackPanel();
            newStackPanel.Margin = new Thickness(10, 0, 0, 0);
            container.Children.Add(newStackPanel);
            newStackPanel.Children.Add(new TextBlock {Text = settingsViewModel.GetType().FullName});
            foreach (var subSection in settingsViewModel.SubSettings)
                AddSettingsEditorPanels(subSection, newStackPanel);
            foreach (var settingViewModel in settingsViewModel.Settings)
            {
                var row = new StackPanel();
                row.Orientation = Orientation.Horizontal;
                row.Children.Add(new TextBlock {Text = settingViewModel.Name});
                // todo: can't go to settings twice because of this line
                row.Children.Add(settingViewModel.PropertyEditor);
                newStackPanel.Children.Add(row);
            }
        }
    }
}