using System.Collections.Generic;

namespace Missile.TextLauncher
{
    public class SettingsViewModel
    {
        public string Name { get; set; }
        public object Instance { get; set; }
        public IList<SettingsViewModel> SubSettings { get; set; } = new List<SettingsViewModel>();
        public IList<SettingViewModel> Settings { get; set; } = new List<SettingViewModel>();
    }
}