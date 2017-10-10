using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
    // todo: extract MVVM pattern
    public partial class TextLauncherImplementation : Launcher, IUiFacade
    {
        private readonly SynchronizationContext _synchronizationContext;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public TextLauncherImplementation()
        {
            _synchronizationContext = SynchronizationContext.Current;
            InitializeComponent();
            InputTextBox.Focus();
        }

        [Import]
        protected internal ILogger Logger { get; set; }

        [Import]
        protected internal IInterpretationFacade InterpretationFacade { get; set; }

        [ImportMany]
        protected internal IRequiresSetup[] ComponentsRequiringSetup { get; set; }

        public void SetOutputControl(FrameworkElement userControl)
        {
            _synchronizationContext.Post(state =>
            {
                OutputPanel.Children.RemoveRange(0, OutputPanel.Children.Count);
                OutputPanel.Children.Add(userControl);
            }, null);
        }

        public void Post(Action<object> command, object argument)
        {
            _synchronizationContext.Post(state => command(state), argument);
        }

        public async void Input_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                Logger.Information(InputTextBox.Text);
                await Task.WhenAll(
                    ComponentsRequiringSetup.Select(c =>
                        c.SetupAsync(cancellationTokenSource.Token)));

                try
                {
                    await InterpretationFacade.ExecuteAsync(InputTextBox.Text, cancellationTokenSource.Token);
                }
                catch (Exception ex)
                {
                    SetOutputControl(new ErrorViewer(ex));
                }
            }
        }
    }
}