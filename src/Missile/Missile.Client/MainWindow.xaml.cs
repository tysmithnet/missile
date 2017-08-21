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
            Center();                                              

            globalEvents = Hook.GlobalEvents();
            appEvents = Hook.AppEvents();
            globalEvents.KeyDown += (sender, args) =>
            {
                Console.WriteLine("global" + args.KeyValue);
            };

            appEvents.KeyDown += (sender, args) =>
            {
                Console.WriteLine("app" + args.KeyValue);
            };
        }

        public void Center()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            Left = (screenWidth / 2) - (Width / 2);
            Top = (screenHeight / 2) - (Height / 2);
        }
    }    
}
