using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gma.System.MouseKeyHook;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Missile.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        private IKeyboardMouseEvents globalEvents;
        private IKeyboardMouseEvents appEvents;

        public MainWindow()
        {
            InitializeComponent();
            SetupKeyListeners();
            Center();

            Result[] results = new[]
            {
                new Result() {Title = "one", Description = "The first"},
                new Result() {Title = "two", Description = "The second"},
                new Result() {Title = "three", Description = "The third"},
            };

            Results.ItemsSource = results;
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
                if(args.KeyCode == Keys.Escape)
                    WindowState = WindowState.Minimized;
            };
        }

        public void Center()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            Left = (screenWidth / 2) - (Width / 2);
            Top = (screenHeight / 2) - (Height / 2);
        }

        private void CommandTextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }    
}
