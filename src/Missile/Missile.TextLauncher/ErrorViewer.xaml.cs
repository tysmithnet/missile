using System;
using System.Text;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Interaction logic for ErrorViewer.xaml
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