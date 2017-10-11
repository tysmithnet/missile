using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Missile.Core;
using Missile.TextLauncher.Annotations;

namespace Missile.TextLauncher
{
    public class TextLauncherImplementationViewModel : INotifyPropertyChanged, IUiFacade
    {
        private string _inputText;

        private Visibility _loadingVisibility;

        private FrameworkElement _outputControl;

        public TextLauncherImplementationViewModel(ILogger logger, IInterpretationFacade interpretationFacade,
            IRequiresSetup[] componentsRequiringSetup)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            InterpretationFacade =
                interpretationFacade ?? throw new ArgumentNullException(nameof(interpretationFacade));
            ComponentsRequiringSetup = componentsRequiringSetup ??
                                       throw new ArgumentNullException(nameof(componentsRequiringSetup));
        }

        protected internal ILogger Logger { get; set; }

        protected internal IInterpretationFacade InterpretationFacade { get; set; }

        protected internal IRequiresSetup[] ComponentsRequiringSetup { get; set; }

        public FrameworkElement OutputControl
        {
            get => _outputControl;
            set
            {
                _outputControl = value;
                OnPropertyChanged();
            }
        }

        public Visibility LoadingVisibility
        {
            get => _loadingVisibility;
            set
            {
                _loadingVisibility = value;
                OnPropertyChanged();
            }
        }

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetOutputControl(FrameworkElement outputControl)
        {
            OutputControl = outputControl;
        }

        public async Task HandleInputKeyDownEventAsync(KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
                await ExecuteCommandAsync();
        }


        public async Task ExecuteCommandAsync()
        {
            Logger.Information(InputText);
            await Task.WhenAll(
                ComponentsRequiringSetup.Select(c =>
                    c.SetupAsync(CancellationToken.None))); // todo: fix

            try
            {
                await InterpretationFacade.ExecuteAsync(InputText, CancellationToken.None);
            }
            catch (Exception ex)
            {
                SetOutputControl(new ErrorViewer(ex));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}