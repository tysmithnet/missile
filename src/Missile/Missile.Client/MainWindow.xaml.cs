using System.Windows;
using Missile.Core;

namespace Missile.Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(Launcher launcher)
        {
            
            InitializeComponent();
            Content = launcher;
        }
    }
}