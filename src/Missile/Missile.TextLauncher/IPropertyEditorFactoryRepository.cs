using System;

namespace Missile.TextLauncher
{
    public interface IPropertyEditorFactoryRepository
    {
        IPropertyEditorFactory Get(Type type);
    }
}