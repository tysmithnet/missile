using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Missile.Core;
using Missile.Core.Logging;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Interaction logic for TextLauncherImplementation.xaml
    /// </summary>
    [Export(typeof(Launcher))]
    [Export(typeof(IUiFacade))]
    public partial class TextLauncherImplementation : Launcher, IUiFacade
    {
        private TextLauncherImplementationViewModel _viewModel;

        public TextLauncherImplementation()
        {
            InitializeComponent();
            InputTextBox.Focus();
        }

        [Import]
        protected internal ILogger Logger { get; set; }

        [Import]
        protected internal IInterpretationFacade InterpretationFacade { get; set; }

        [ImportMany]
        protected internal IRequiresSetup[] ComponentsRequiringSetup { get; set; }

        public void SetOutputControl(FrameworkElement outputControl)
        {
            _viewModel.SetOutputControl(outputControl);
        }

        public async void Input_OnKeyDown(object sender, KeyEventArgs e)
        {
            await _viewModel.HandleInputKeyDownEventAsync(e);
        }

        private void TextLauncherImplementation_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel =
                new TextLauncherImplementationViewModel(Logger, InterpretationFacade, ComponentsRequiringSetup);
            DataContext = _viewModel;
        }
    }
}