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
    /// <inheritdoc cref="IUiFacade" />
    /// <inheritdoc cref="INotifyPropertyChanged" />
    /// <summary>
    ///     View model for TextLauncherImplementation
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="T:Missile.TextLauncher.IUiFacade" />
    public class TextLauncherImplementationViewModel : INotifyPropertyChanged, IUiFacade
    {
        /// <summary>
        ///     The input text
        /// </summary>
        private string _inputText;

        /// <summary>
        ///     The visibility of the loading icon
        /// </summary>
        private Visibility _loadingVisibility = Visibility.Hidden;

        /// <summary>
        ///     The output control
        /// </summary>
        private FrameworkElement _outputControl;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextLauncherImplementationViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger to use</param>
        /// <param name="interpretationFacade">The interpretation facade to use</param>
        /// <param name="componentsRequiringSetup">The components requiring setup</param>
        /// <exception cref="ArgumentNullException">
        ///     logger
        ///     or
        ///     interpretationFacade
        ///     or
        ///     componentsRequiringSetup
        /// </exception>
        public TextLauncherImplementationViewModel(ILogger logger, IInterpretationFacade interpretationFacade,
            IRequiresSetup[] componentsRequiringSetup)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            InterpretationFacade =
                interpretationFacade ?? throw new ArgumentNullException(nameof(interpretationFacade));
            ComponentsRequiringSetup = componentsRequiringSetup ??
                                       throw new ArgumentNullException(nameof(componentsRequiringSetup));
        }

        /// <summary>
        ///     Gets or sets the logger
        /// </summary>
        /// <value>
        ///     The logger
        /// </value>
        protected internal ILogger Logger { get; set; }

        /// <summary>
        ///     Gets or sets the interpretation facade
        /// </summary>
        /// <value>
        ///     The interpretation facade
        /// </value>
        protected internal IInterpretationFacade InterpretationFacade { get; set; }

        /// <summary>
        ///     Gets or sets the components requiring setup
        /// </summary>
        /// <value>
        ///     The components requiring setup
        /// </value>
        protected internal IRequiresSetup[] ComponentsRequiringSetup { get; set; }

        /// <summary>
        ///     Gets or sets the output control
        /// </summary>
        /// <value>
        ///     The output control
        /// </value>
        public FrameworkElement OutputControl
        {
            get => _outputControl;
            set
            {
                _outputControl = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the loading visibility
        /// </summary>
        /// <value>
        ///     The loading visibility
        /// </value>
        public Visibility LoadingVisibility
        {
            get => _loadingVisibility;
            set
            {
                _loadingVisibility = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the input text
        /// </summary>
        /// <value>
        ///     The input text
        /// </value>
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc />
        /// <summary>
        ///     Sets the output control
        /// </summary>
        /// <param name="outputControl">The output control</param>
        public void SetOutputControl(FrameworkElement outputControl)
        {
            OutputControl = outputControl;
        }

        /// <summary>
        ///     Handles the input key down event asynchronously
        /// </summary>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data</param>
        /// <returns>A Task that when complete will indicate the event was handled</returns>
        public async Task HandleInputKeyDownEventAsync(KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
                await ExecuteCommandAsync();
        }

        /// <summary>
        ///     Executes the command asynchronously
        /// </summary>
        /// <returns>A Task that when complete will indicate the event was handled</returns>
        public async Task ExecuteCommandAsync()
        {
            Logger.Information(InputText);
            await Task.WhenAll(
                ComponentsRequiringSetup.Select(c =>
                    c.SetupAsync(CancellationToken.None))); // todo: fix

            try
            {
                LoadingVisibility = Visibility.Visible;
                await InterpretationFacade.ExecuteAsync(InputText, CancellationToken.None);
                LoadingVisibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                SetOutputControl(new ErrorViewer(ex));
            }
        }

        /// <summary>
        ///     Called when [property changed]
        /// </summary>
        /// <param name="propertyName">Name of the property that changed</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}