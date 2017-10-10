using System;
using System.Windows;

namespace Missile.TextLauncher
{
    public interface IUiFacade
    {
        void SetOutputControl(FrameworkElement userControl);
        void Post(Action<object> command, object argument);
    }
}