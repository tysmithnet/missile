using System;

namespace Missile.Client.TextLauncher.Compilation
{
    public interface IConverterRepository
    {
        IConverter Get(Type source, Type dest);                       
    }
}
