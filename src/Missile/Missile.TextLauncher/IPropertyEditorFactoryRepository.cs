using System;
using System.Windows.Controls;

namespace Missile.TextLauncher
{
    public interface IPropertyEditorFactoryRepository
    {
        IPropertyEditorFactory Get(Type type);
    }
}