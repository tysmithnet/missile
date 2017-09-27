using System;
using System.Windows;

namespace Missile.TextLauncher
{
    public interface IPropertyEditorFactory
    {
        bool CanHandle(Type type);
        UIElement GetControl(PropertyFieldAdapter adapter);
    }
}