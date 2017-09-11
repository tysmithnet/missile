using System;

namespace Missile.TextLauncher
{
    public interface IConverterRepository
    {
        RegisteredConverter Get(Type source, Type dest);
    }
}