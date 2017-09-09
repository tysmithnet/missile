using System;
using System.ComponentModel.Composition;
using System.Windows;
using Missile.Core;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Interaction logic for TextLauncherImplementation.xaml
    /// </summary>
    [Export(typeof(Launcher))]
    public partial class TextLauncherImplementation : Launcher
    {
        public TextLauncherImplementation()
        {
            InitializeComponent();
        }

        private void TextLauncherImplementation_OnLayoutUpdated(object sender, EventArgs e)
        {
            Input.Focus();
        }
    }
}