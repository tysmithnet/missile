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
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gma.System.MouseKeyHook;

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
            CenterWindowOnScreen();
            IntPtr handle = new WindowInteropHelper(this).Handle;

            globalEvents = Hook.GlobalEvents();
            appEvents = Hook.AppEvents();
            globalEvents.KeyDown += (sender, args) =>
            {
                Console.WriteLine(args.KeyValue);
            };

            appEvents.KeyDown += (sender, args) =>
            {
                Console.WriteLine(args.KeyValue);
            };
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
    }    
}
