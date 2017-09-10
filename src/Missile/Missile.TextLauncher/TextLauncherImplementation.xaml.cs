using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Input;
using Missile.Core;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Interaction logic for TextLauncherImplementation.xaml
    /// </summary>
    [Export(typeof(Launcher))]
    [Export(typeof(IUiFacade))]
    public partial class TextLauncherImplementation : Launcher, IUiFacade
    {
        public TextLauncherImplementation()
        {
            InitializeComponent();
        }

        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }

        [Import(typeof(IInterpretationFacade))]
        public IInterpretationFacade InterpretationFacade { get; set; }
                                             
        // TODO: hack
        private void TextLauncherImplementation_OnLayoutUpdated(object sender, EventArgs e)
        {
            Input.Focus();
        }

        private async void Input_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                Logger.Information(Input.Text);
                await InterpretationFacade.ExecuteAsync(Input.Text);
            }
        }

        public void SetOutputControl(UserControl userControl)
        {
            OutputPanel.Children.RemoveRange(0, OutputPanel.Children.Count);
            OutputPanel.Children.Add(userControl);
        }
    }
}