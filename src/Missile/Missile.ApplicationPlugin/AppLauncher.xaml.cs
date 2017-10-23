using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using Missile.Core;
using Missile.Core.Properties;

namespace Missile.ApplicationPlugin
{
    /// <summary>
    ///     Interaction logic for AppLauncher.xaml
    /// </summary>
    [Export(typeof(Launcher))]
    public partial class AppLauncher : Launcher
    {
        public AppLauncher()
        {
            InitializeComponent();
        }

        [Import]
        protected internal IApplicationRepository ApplicationRepository { get; set; }

        private void AppLauncher_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AppLauncherViewModel(ApplicationRepository);
        }
    }

    public class AppLauncherViewModel : INotifyPropertyChanged
    {
        private string _searchText;

        public AppLauncherViewModel(IApplicationRepository applicationRepository)
        {
            ApplicationRepository = applicationRepository;
            Applications = new ObservableCollection<RegisteredApplicationViewModel>(applicationRepository.GetAll()
                .Select(a => new RegisteredApplicationViewModel(a)));
        }

        protected internal IApplicationRepository ApplicationRepository { get; set; }

        public ObservableCollection<RegisteredApplicationViewModel> Applications { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Applications.Clear();
                foreach (var applicationViewModel in ApplicationRepository.GetAll()
                    .Where(a => a.ApplicationName.Contains(value)).Select(a => new RegisteredApplicationViewModel(a)))
                    Applications.Add(applicationViewModel);

                OnPropertyChanged();
                OnPropertyChanged(nameof(Applications));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RegisteredApplicationViewModel : INotifyPropertyChanged
    {
        public RegisteredApplicationViewModel(RegisteredApplication registeredApplication)
        {
            RegisteredApplication =
                registeredApplication ?? throw new ArgumentNullException(nameof(registeredApplication));
        }

        public RegisteredApplication RegisteredApplication { get; set; }

        public ImageSource Icon
        {
            get => RegisteredApplication.Icon;
            set => RegisteredApplication.Icon = value;
        }

        public string ApplicationName
        {
            get => RegisteredApplication.ApplicationName;
            set => RegisteredApplication.ApplicationName = value;
        }

        public string ApplicationPath
        {
            get => RegisteredApplication.ApplicationPath;
            set => RegisteredApplication.ApplicationPath = value;
        }

        public Visibility Visibility { get; set; } = Visibility.Visible;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}