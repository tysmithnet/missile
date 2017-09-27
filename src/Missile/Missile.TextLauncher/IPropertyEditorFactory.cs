using System;
using System.Windows;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    public interface IPropertyEditorFactory
    {
        bool CanHandle(Type type);
        UIElement GetControl(PropertyFieldAdapter adapter);
    }
}