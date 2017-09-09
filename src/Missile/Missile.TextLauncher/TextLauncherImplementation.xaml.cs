using System.ComponentModel.Composition;
using Missile.Core;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Interaction logic for TextLauncherImplementation.xaml
    /// </summary>
    [Export(typeof(Launcher))]
    public partial class TextLauncherImplementation : Launcher
    {
        public TextLauncherImplementation()
        {
            InitializeComponent();
        }
    }
}