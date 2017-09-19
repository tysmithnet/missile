using System;

namespace Missile.TextLauncher.Conversion
{
    public interface IConverterRepository
    {
        RegisteredConverter Get(Type source, Type dest);
    }
}