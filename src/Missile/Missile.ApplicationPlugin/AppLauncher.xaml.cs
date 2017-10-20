using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Missile.Core;
using Missile.Core.Properties;

namespace Missile.ApplicationPlugin
{                       
    /// <summary>
    /// Interaction logic for AppLauncher.xaml
    /// </summary>
    [Export(typeof(Launcher))]
    public partial class AppLauncher : Launcher
    {
        [Import]
        protected internal IApplicationRepository ApplicationRepository { get; set; }

        public AppLauncher()
        {                                               
            InitializeComponent();
        }

        private void AppLauncher_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AppLauncherViewModel(ApplicationRepository);
        }
    }

    public class AppLauncherViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        protected internal IApplicationRepository ApplicationRepository { get; set; }
        
        public ObservableCollection<RegisteredApplicationViewModel> Applications { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public AppLauncherViewModel(IApplicationRepository applicationRepository)
        {
            ApplicationRepository = applicationRepository;
            Applications = new ObservableCollection<RegisteredApplicationViewModel>(applicationRepository.GetAll().Select(a => new RegisteredApplicationViewModel(a)));            
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                Applications.Clear();
                foreach (var applicationViewModel in ApplicationRepository.GetAll().Where(a => a.ApplicationName.Contains(value)).Select(a => new RegisteredApplicationViewModel(a)))
                {
                    Applications.Add(applicationViewModel);
                }
                
                OnPropertyChanged();
                OnPropertyChanged(nameof(Applications));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RegisteredApplicationViewModel : INotifyPropertyChanged
    {
        public RegisteredApplication RegisteredApplication { get; set; }

        public RegisteredApplicationViewModel(RegisteredApplication registeredApplication)
        {
            RegisteredApplication = registeredApplication ?? throw new ArgumentNullException(nameof(registeredApplication));
        }

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
