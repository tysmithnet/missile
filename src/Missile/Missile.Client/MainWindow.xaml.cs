using System.Windows;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Missile.Core;

namespace Missile.Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IKeyboardMouseEvents appEvents;
        private IKeyboardMouseEvents globalEvents;

        public MainWindow(Launcher launcher)
        {
            InitializeComponent();
            SetupKeyListeners();
            Content = launcher;
        }

        private void SetupKeyListeners()
        {
            globalEvents = Hook.GlobalEvents();
            appEvents = Hook.AppEvents();
            globalEvents.KeyDown += (sender, args) =>
            {
                if (args.Alt && args.KeyCode == Keys.Space)
                {
                    WindowState = WindowState.Normal;
                    Activate();
                    Topmost = true;
                    args.Handled = true;
                }
            };

            appEvents.KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Escape)
                    WindowState = WindowState.Minimized;
            };
        }
    }
}