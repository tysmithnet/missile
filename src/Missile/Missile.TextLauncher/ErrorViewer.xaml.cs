using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Missile.TextLauncher
{
    /// <summary>
    /// Interaction logic for ErrorViewer.xaml
    /// </summary>
    public partial class ErrorViewer : UserControl
    {
        public ErrorViewer(Exception exception)
        {
            InitializeComponent();
            var builder = new StringBuilder();
            builder
                .AppendLine(exception.GetType().FullName)
                .AppendLine(exception.Message)
                .AppendLine(exception.StackTrace);
            OutputLabel.Content = builder.ToString();
        }
    }
}
