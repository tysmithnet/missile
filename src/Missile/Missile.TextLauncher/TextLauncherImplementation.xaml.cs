using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Missile.Core;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Interaction logic for TextLauncherImplementation.xaml
    /// </summary>
    [Export(typeof(Launcher))]
    public partial class TextLauncherImplementation : Launcher
    {
        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }

        public string Text { get; set; }

        public TextLauncherImplementation()
        {                                     
            InitializeComponent();
        }

        // TODO: hack
        private void TextLauncherImplementation_OnLayoutUpdated(object sender, EventArgs e)
        {
            Input.Text = DateTime.Now.ToLongTimeString();
            Input.Focus();
        }

        private void Input_OnKeyDown(object sender, KeyEventArgs e)
        {
            Logger.Information(Input.Text);
        }
    }
}