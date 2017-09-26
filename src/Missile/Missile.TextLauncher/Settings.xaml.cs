﻿using System;
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
        public SettingsViewModel SettingsViewModel { get; set; }

        public Settings(SettingsViewModel settings)
        {
            SettingsViewModel = settings;
            InitializeComponent();
            ItemsListBox.ItemsSource = new object[] {settings.Instance.GetType().FullName};
        }
    }
}
