using System;
using System.Windows;

namespace Missile.TextLauncher
{
    public interface IPropertyEditorFactory
    {
        bool CanHandle(Type type);
        FrameworkElement GetControl(PropertyFieldAdapter adapter);
    }
}