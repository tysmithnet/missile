using System;
using System.Collections.Generic;

namespace Missile.Client.TextLauncher.Compilation
{
    public interface IConverterRepository
    {
        IEnumerable<IConverter> Get(Type sourceType, Type destType);
    }
}