using System;

namespace Missile.TextLauncher
{
    public interface IConverter
    {
        bool CanHandleRequest(Type sourceType, Type destType);
        object Convert(object source, Type sourceType, Type destType);
    }
}