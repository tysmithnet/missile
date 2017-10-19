using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Missile.Core;

namespace Missile.Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class MainWindow : Window
    {
        protected internal IKeyboardMouseEvents AppEvents;
        protected internal IKeyboardMouseEvents GlobalEvents;

        public MainWindow(Launcher launcher)
        {
            InitializeComponent();
            SetupKeyListeners();
            Content = launcher;
        }

        private void SetupKeyListeners()
        {
            GlobalEvents = Hook.GlobalEvents();
            AppEvents = Hook.AppEvents();
            GlobalEvents.KeyDown += (sender, args) =>
            {
                if (!args.Alt || args.KeyCode != Keys.Space) return;
                WindowState = WindowState.Normal;
                Activate();
                Topmost = true;
                args.Handled = true;
            };

            AppEvents.KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Escape)
                    WindowState = WindowState.Minimized;
            };
        }
    }
}