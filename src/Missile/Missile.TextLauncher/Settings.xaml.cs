using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Missile.TextLauncher
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public IList<SettingsViewModel> SettingsViewModels { get; set; }

        public Settings(IEnumerable<SettingsViewModel> settings)
        {
            SettingsViewModels = settings.ToList();
            InitializeComponent();
            foreach (var settingsViewModel in SettingsViewModels)
            {
                AddSettingsEditorPanels(settingsViewModel, SettingsPanel);
            }
        }

        private void AddSettingsEditorPanels(SettingsViewModel settingsViewModel, StackPanel container)
        {
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Margin = new Thickness(10, 0, 0, 0);
            container.Children.Add(newStackPanel);
            newStackPanel.Children.Add(new TextBlock(){Text = settingsViewModel.GetType().FullName });
            foreach (var subSection in settingsViewModel.SubSettings)
            {                                                                                                
                AddSettingsEditorPanels(subSection, newStackPanel);
            }
            foreach (var settingViewModel in settingsViewModel.Settings)
            {
                StackPanel row = new StackPanel();
                row.Orientation = Orientation.Horizontal;
                row.Children.Add(new TextBlock() {Text = settingViewModel.Name});
                row.Children.Add(settingViewModel.PropertyEditor);
                newStackPanel.Children.Add(row);
            }
        }
    }
}
