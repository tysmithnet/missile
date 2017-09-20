using System;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    public interface IUiFacade
    {
        void SetOutputControl(UserControl userControl);
        void Post(Action<object> command, object argument);
    }
}